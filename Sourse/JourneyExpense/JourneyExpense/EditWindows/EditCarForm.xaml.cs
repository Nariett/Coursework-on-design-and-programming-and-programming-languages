using JourneyExpense.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;

namespace JourneyExpense
{
    /// <summary>
    /// Логика взаимодействия для EditCarForm.xaml
    /// </summary>
    public partial class EditCarForm : Window
    {
        private List<Car> AllCar = new List<Car>();
        private List<Fuel> AllFuel = new List<Fuel>();
        private bool Type = false;
        public EditCarForm()
        {
            InitializeComponent();
            ReadData();
            InitComboBox();
        }
        private void ReadData()//инициализация списков 
        {
            AllCar = Car.ReadCarInXML();
            AllCar = AllCar.OrderBy(car => car.Name).ToList();
            AllFuel = Fuel.ReadFuelInXML();
        }
        private void InitComboBox()//инициализация comboBox 
        {
            foreach (var item in AllCar)
            {
                comboBoxCar.Items.Add(item.Name);
            }
        }
        private void comboBoxCar_SelectionChanged(object sender, SelectionChangedEventArgs e)//обработчик изменения элементов в comboBox
        {
            if (comboBoxCar.SelectedIndex != -1)
            {
                foreach (var item in AllCar)
                {
                    if (item.Name == comboBoxCar.SelectedItem.ToString())
                    {
                        textBoxFuelConsumptionCity.Text = item.FuelConsumptionCity.ToString();
                        textBoxFuelConsumptionGeneral.Text = item.FuelConsumptionCity.ToString();
                        textBoxFuelConsumptionHighway.Text = item.FuelConsumptionHighway.ToString();
                        textBoxPower.Text = item.EnginePower.ToString();
                        comboBoxOctan.Items.Clear();
                        comboBoxOctan.IsEnabled = true;
                        textBoxFuelConsumptionCity.Visibility = Visibility.Visible;
                        textBoxFuelConsumptionHighway.Visibility = Visibility.Visible;
                        if (item.Fuel == "Электричество")
                        {
                            Type = true;
                            comboBoxOctan.IsEnabled = false;
                            textBoxFuelConsumptionCity.Visibility = Visibility.Hidden;
                            textBoxFuelConsumptionHighway.Visibility = Visibility.Hidden;
                        }
                        else if (item.Fuel == "Дизельное топливо")
                        {
                            Type = false;
                            comboBoxOctan.IsEnabled = true;
                            List<Fuel> filteredList = AllFuel.Where(fuel => fuel.name == "Дизельное топливо").ToList();
                            foreach (var fuel in filteredList)
                            {
                                comboBoxOctan.Items.Add(fuel.octaneNumber);
                            }
                        }
                        else if (item.Fuel == "Бензин")
                        {
                            Type = false;
                            comboBoxOctan.IsEnabled = true;
                            List<Fuel> filteredList = AllFuel.Where(fuel => fuel.name == "Бензин").ToList();
                            foreach (var fuel in filteredList)
                            {
                                comboBoxOctan.Items.Add(fuel.octaneNumber);
                            }
                        }
                    }
                }
            }
        }

        private void UpdateCarButton_Click(object sender, RoutedEventArgs e)//обновленение характеритик автомобиля 
        {
            if (IsValidComboBox(comboBoxOctan) & СonsumptionСheck())
            {
                string car = comboBoxCar.SelectedItem.ToString();
                string fuelConsumptionGeneral = FixStr(textBoxFuelConsumptionGeneral.Text);
                string fuelConsumptionCity = FixStr(textBoxFuelConsumptionCity.Text);
                string fuelConsumptionHighway = FixStr(textBoxFuelConsumptionHighway.Text);
                string enginePower = FixStr(textBoxPower.Text);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("Car.xml");
                XmlNode carNode = xmlDoc.SelectSingleNode($"//car[name='{car}']");

                if (Type)
                {
                    carNode.SelectSingleNode("fuelConsumptionGeneral").InnerText = fuelConsumptionGeneral;
                    carNode.SelectSingleNode("fuelConsumptionCity").InnerText = fuelConsumptionGeneral;
                    carNode.SelectSingleNode("fuelConsumptionHighway").InnerText = fuelConsumptionGeneral;
                }
                else
                {
                    carNode.SelectSingleNode("fuelConsumptionGeneral").InnerText = fuelConsumptionGeneral;
                    carNode.SelectSingleNode("fuelConsumptionCity").InnerText = fuelConsumptionCity;
                    carNode.SelectSingleNode("fuelConsumptionHighway").InnerText = fuelConsumptionHighway;

                }
                carNode.SelectSingleNode("enginePower").InnerText = enginePower;

                // Сохранение изменений в XML-документе
                xmlDoc.Save("Car.xml");
                MessageBox.Show($"Характеристики автомобиля {comboBoxCar.SelectedItem.ToString()} обновлены.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Ошибка ввода данных. Повторите попытку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool СonsumptionСheck()//проверка значений
        {
            double ConsumptionGeneral, ConsumptionCity, ConsumptionHighway, power;
            bool isNumericOne = IsValidDoubleInput(textBoxFuelConsumptionGeneral, 0, 100, out ConsumptionGeneral);
            bool isNumericTwo = IsValidDoubleInput(textBoxFuelConsumptionCity, 0, 100, out ConsumptionCity);
            bool isNumericThree = IsValidDoubleInput(textBoxFuelConsumptionHighway, 0, 100, out ConsumptionHighway);
            bool isNumericFour = IsValidDoubleInput(textBoxFuelConsumptionHighway, 0, 2000, out power);
            if (isNumericOne & isNumericTwo & isNumericThree & isNumericFour)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsValidDoubleInput(TextBox box, int min, int max, out double value)//проверка вещественных значений 
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
        private bool IsValidComboBox(ComboBox box)//проверка корретконсти знначений в comboBox
        {
            if (box.SelectedIndex == -1)
            {
                if (!Type)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return true;
            }
        }
        private string FixStr(string input)//замена . на ,
        {
            return input.Replace('.', ',');
        }

        private void DeleteCarButton_Click(object sender, RoutedEventArgs e)// удаление автомобиля
        {
            if (comboBoxCar.SelectedIndex != -1)
            {
                XDocument xdoc = XDocument.Load("Car.xml");

                // Определите условия фильтрации
                string car = comboBoxCar.SelectedItem.ToString();
                MessageBoxResult result = MessageBox.Show($"Автомобиль {car} будет удален из системы. Желаете продолжить?", "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    IEnumerable<XElement> fuelElements = xdoc.Descendants("car")
                    .Where(e => e.Element("name")?.Value == car);
                    // Удаляем найденные элементы
                    foreach (XElement fuelElement in fuelElements.ToList())
                    {
                        fuelElement.Remove();
                    }
                    // Сохраняем изменения обратно в XML-файл
                    xdoc.Save("Car.xml");
                    MessageBox.Show($"Автомобиль {car} был успешно удален из системы.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    // Код для отмены выполнения
                    MessageBox.Show("Удаление отменено.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Автомобиль не выбран. Повторите попытку.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
