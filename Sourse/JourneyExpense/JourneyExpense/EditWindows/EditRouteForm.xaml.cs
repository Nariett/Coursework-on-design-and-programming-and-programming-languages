using JourneyExpense.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;

namespace JourneyExpense.EditWindows
{
    /// <summary>
    /// Логика взаимодействия для EditRouteForm.xaml
    /// </summary>
    public partial class EditRouteForm : Window
    {
        private List<Route> AllRoutes = new List<Route>();
        private List<string> PointA = new List<string>();
        private List<string> PointB = new List<string>();
        private bool isMessageBoxShowPoint = false;

        public EditRouteForm()
        {
            InitializeComponent();
            ReadData();
            InitComboBox();
        }
        private void ReadData()//инициализация списка 
        {
            AllRoutes.Clear();
            AllRoutes = Route.ReadRousteInXML();
            foreach (var item in AllRoutes)
            {
                if (!PointA.Contains(item.PointA))
                {
                    PointA.Add(item.PointA);
                }
                if (!PointA.Contains(item.PointB))
                {
                    PointA.Add(item.PointB);
                }
                if (!PointB.Contains(item.PointA))
                {
                    PointB.Add(item.PointA);
                }
                if (!PointB.Contains(item.PointB))
                {
                    PointB.Add(item.PointB);
                }
            }
            PointA = PointA.OrderBy(item => item).ToList();
            PointB = PointB.OrderBy(item => item).ToList();
        }
        private void InitComboBox()//иницилия combBox
        {
            comboBoxPointOne.Items.Clear();
            comboBoxPointTwo.Items.Clear();
            foreach (var item in PointA)
            {
                comboBoxPointOne.Items.Add(item);
            }
            foreach (var item in PointB)
            {
                comboBoxPointTwo.Items.Add(item);
            }
        }

        private void EditRouteButton_Click(object sender, RoutedEventArgs e)//редактировать данные в маршрутах 
        {
            double distance = 0;
            if (comboBoxPointOne.SelectedIndex != -1 && comboBoxPointTwo.SelectedIndex != -1 && IsValidDoubleInput(textBoxDistance, 0, 10000, out distance))
            {
                string pointA = comboBoxPointOne.SelectedItem.ToString();
                string pointB = comboBoxPointTwo.SelectedItem.ToString();
                string distanceStr = FixStr(distance.ToString());
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("Routes.xml");
                XmlNode carNode = xmlDoc.SelectSingleNode($"//route[(PointA='{pointA}' and PointB='{pointB}') or (PointA='{pointB}' and PointB='{pointA}')]");
                carNode.SelectSingleNode("kilometer").InnerText = distanceStr;
                xmlDoc.Save("Routes.xml");
                MessageBox.Show($"Дистанция маршрута {pointA} - {pointB} обновлена.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Ошибка ввода данных. Повторите попытку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void DeleteRouteButton_Click(object sender, RoutedEventArgs e)//удалить маршрут 
        {
            if (comboBoxPointOne.SelectedIndex != -1 && comboBoxPointTwo.SelectedIndex != -1)
            {
                XDocument xdoc = XDocument.Load("Routes.xml");

                // Определите условия фильтрации
                string pointA = comboBoxPointOne.SelectedItem.ToString();
                string pointB = comboBoxPointTwo.SelectedItem.ToString();
                MessageBoxResult result = MessageBox.Show($"Маршрут {pointA} - {pointB} будет удален из системы. Желаете продолжить?", "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    IEnumerable<XElement> routesElements = xdoc.Descendants("route")
                    .Where(e => (e.Element("PointA")?.Value == pointA && e.Element("PointB")?.Value == pointB) | (e.Element("PointA")?.Value == pointB && e.Element("PointB")?.Value == pointA));

                    // Удаляем найденные элементы
                    foreach (XElement routeElement in routesElements.ToList())
                    {
                        routeElement.Remove();
                    }
                    // Сохраняем изменения обратно в XML-файл
                    xdoc.Save("Routes.xml");
                    MessageBox.Show($"Маршрут {pointA} - {pointB} был успешно удален из системы.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    // Код для отмены выполнения
                    MessageBox.Show("Удаление отменено.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Маршрут не выбран. Повторите попытку.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ReadData();
            InitComboBox();
            textBoxDistance.Clear();
        }

        private void comboBoxPointOne_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (comboBoxPointOne.SelectedIndex != -1 && comboBoxPointTwo.SelectedIndex != -1 && comboBoxPointOne.SelectedItem != comboBoxPointTwo.SelectedItem)
            {
                string A = comboBoxPointOne.SelectedItem.ToString();
                string B = comboBoxPointTwo.SelectedItem.ToString();
                isMessageBoxShowPoint = false;
                if (A != B)
                {
                    foreach (var item in AllRoutes)
                    {
                        if ((item.PointA == A && item.PointB == B) || (item.PointA == B && item.PointB == A))
                        {
                            this.textBoxDistance.Text = item.Distance.ToString();
                            isMessageBoxShowPoint = true;
                            break;
                        }
                    }
                    if (!isMessageBoxShowPoint)
                    {
                        MessageBox.Show("Выбранный маршрут не найден. Повторите попытку.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        comboBoxPointOne.SelectedIndex = -1;
                        textBoxDistance.Clear();
                    }
                }
            }
            else if (comboBoxPointOne.SelectedIndex == -1 || comboBoxPointTwo.SelectedIndex == -1)
            {

            }
            else if (comboBoxPointOne.SelectedIndex != -1 || comboBoxPointTwo.SelectedIndex != -1)
            {
                MessageBox.Show("Выберите корректное место назначения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                comboBoxPointOne.SelectedIndex = -1;
                textBoxDistance.Clear();
            }
        }
        private void comboBoxPointTwo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (comboBoxPointOne.SelectedIndex != -1 && comboBoxPointTwo.SelectedIndex != -1 && comboBoxPointOne.SelectedItem != comboBoxPointTwo.SelectedItem)
            {
                string A = comboBoxPointOne.SelectedItem.ToString();
                string B = comboBoxPointTwo.SelectedItem.ToString();
                isMessageBoxShowPoint = false;
                if (A != B)
                {
                    foreach (var item in AllRoutes)
                    {
                        if ((item.PointA == A && item.PointB == B) || (item.PointA == B && item.PointB == A))
                        {
                            this.textBoxDistance.Text = item.Distance.ToString();
                            isMessageBoxShowPoint = true;
                            break;
                        }
                    }
                    if (!isMessageBoxShowPoint)
                    {
                        MessageBox.Show("Выбранный маршрут не найден. Повторите попытку.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        comboBoxPointTwo.SelectedIndex = -1;
                        textBoxDistance.Clear();
                    }
                }
            }
            else if (comboBoxPointOne.SelectedIndex == -1 || comboBoxPointTwo.SelectedIndex == -1)
            {

            }
            else if (comboBoxPointOne.SelectedIndex != -1 || comboBoxPointTwo.SelectedIndex != -1)
            {
                MessageBox.Show("Выберите корректное место назначения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                comboBoxPointTwo.SelectedIndex = -1;
                textBoxDistance.Clear();
            }
        }

        private void comboBoxPointOne_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)//очистка значений из coomboBox
        {
            comboBoxPointOne.SelectedIndex = -1;
        }

        private void comboBoxPointTwo_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)//очистка значений из coomboBox
        {
            comboBoxPointTwo.SelectedIndex = -1;
        }
        private bool IsValidDoubleInput(TextBox box, int min, int max, out double value)//проверка корректности вещественных значений 
        {
            value = 0;
            if (box.Text == "")
            {
                box.BorderBrush = Brushes.Red;
                return false;


            }
            bool isNumeric = double.TryParse(FixStr(box.Text), out value);
            if (isNumeric)
            {
                if (value > min && value < max)
                {
                    box.BorderBrush = Brushes.Gray;
                    return true;
                }
                else
                {
                    box.BorderBrush = Brushes.Red;
                    return false;
                }
            }
            else
            {
                box.BorderBrush = Brushes.Red;
                return false;
            }
        }
        private string FixStr(string input)//замена . на ,
        {
            return input.Replace('.', ',');
        }
    }
}
