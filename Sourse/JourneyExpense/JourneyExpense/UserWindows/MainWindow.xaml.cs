using JourneyExpense.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace JourneyExpense
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string userName, string surname)
        {
            UserName = userName;
            Surname = surname;
            InitializeComponent();
            InitList();
            InitComboBox();
        }
        private string UserName;
        private string Surname;
        private bool isMessageBoxShown = false;
        private bool isMessageBoxShowPoint = false;
        private List<string> FuelList = new List<string>() { "Бензин", "Дизельное топливо", "Электричество" };
        private List<string> TypeConsuption = new List<string> { "Городской", "По трассе", "Смешанный" };
        private List<Car> CarList = new List<Car>();
        private List<Fuel> PriceList = new List<Fuel>();
        private List<Route> AllRoutes = new List<Route>();
        private List<string> PointA = new List<string>();
        private List<string> PointB = new List<string>();

        public void InitList()//инициализация списков
        {
            CarList.Clear();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("Car.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                Car car = new Car();
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "name")
                    {
                        car.Name = childnode.InnerText;
                    }
                    if (childnode.Name == "year")
                    {
                        car.Year = Convert.ToInt32(childnode.InnerText);
                    }
                    if (childnode.Name == "typeCar")
                    {
                        car.TypeCar = childnode.InnerText;
                    }
                    if (childnode.Name == "maxSpeed")
                    {
                        car.MaxSpeed = Convert.ToInt32(childnode.InnerText);
                    }
                    if (childnode.Name == "seatingCapacity")
                    {
                        car.SeatingCapacity = Convert.ToInt32(childnode.InnerText);
                    }
                    if (childnode.Name == "fuel")
                    {
                        car.Fuel = childnode.InnerText;
                    }
                    if (childnode.Name == "fuelOctan")
                    {
                        car.FuelOctan = childnode.InnerText;
                    }
                    if (childnode.Name == "fuelConsumptionGeneral")
                    {
                        car.FuelConsumptionGeneral = Convert.ToDouble(childnode.InnerText);
                    }
                    if (childnode.Name == "fuelConsumptionCity")
                    {
                        car.FuelConsumptionCity = Convert.ToDouble(childnode.InnerText);
                    }
                    if (childnode.Name == "fuelConsumptionHighway")
                    {
                        car.FuelConsumptionHighway = Convert.ToDouble(childnode.InnerText);
                    }
                    if (childnode.Name == "enginePower")
                    {
                        car.EnginePower = Convert.ToDouble(childnode.InnerText);
                    }
                    if (childnode.Name == "tankSize")
                    {
                        car.TankSize = Convert.ToDouble(childnode.InnerText);
                    }
                }
                CarList.Add(car);
            }
            CarList = CarList.OrderBy(car => car.Name).ToList();
            PriceList.Clear();
            XmlDocument xDocTwo = new XmlDocument();
            xDocTwo.Load("Fuel.xml");
            XmlElement xRootTwo = xDocTwo.DocumentElement;
            foreach (XmlNode xnode in xRootTwo)
            {
                Fuel fuel = new Fuel();
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "name")
                    {
                        fuel.name = childnode.InnerText;
                    }
                    if (childnode.Name == "octaneNumber")
                    {
                        fuel.octaneNumber = childnode.InnerText;
                    }
                    if (childnode.Name == "price")
                    {
                        fuel.price = Convert.ToDouble(childnode.InnerText);
                    }
                }
                PriceList.Add(fuel);
            }
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
        public void InitComboBox() // инициализация comboBox
        {
            foreach (var item in FuelList)
            {
                comboBoxFuelType.Items.Add(item);
            }
            foreach (var item in TypeConsuption)
            {
                comboBoxConsumption.Items.Add(item);
            }
            foreach (var item in CarList)
            {
                comboBoxCar.Items.Add(item.Name);
            }
            comboBoxConsumption.IsEnabled = false;
            foreach (var item in PointA)
            {
                comboBoxPointOne.Items.Add(item);
            }
            foreach (var item in PointB)
            {
                comboBoxPointTwo.Items.Add(item);
            }
        }

        private void comboBoxFuelType_SelectionChanged_1(object sender, SelectionChangedEventArgs e)// проверка значений comboBoxFuelType
        {
            if (comboBoxFuelType.SelectedIndex != -1)
            {
                LabelLitr.Content = "Литров";
                string selectedItem = comboBoxFuelType.SelectedItem.ToString();
                if (selectedItem == "Бензин")
                {
                    comboBoxFuelOctan.ToolTip = "Октановое число";
                    comboBoxFuelOctan.ItemsSource = GetFuelList("Бензин");
                    LabelConsumption.Content = "Литр";
                }
                else if (selectedItem == "Дизельное топливо")
                {
                    comboBoxFuelOctan.ToolTip = "Октановое число";
                    comboBoxFuelOctan.ItemsSource = GetFuelList("Дизельное топливо");
                    LabelConsumption.Content = "Литр";
                }
                else if (selectedItem == "Электричество")
                {
                    comboBoxFuelOctan.ToolTip = "Вид зарядки";
                    comboBoxFuelOctan.ItemsSource = GetFuelList("Электричество");
                    LabelConsumption.Content = "Вт-ч";
                    LabelLitr.Content = "Вт-ч";
                }
                comboBoxFuelOctan.SelectedIndex = 1;
            }
        }
        private string SetValue(ComboBox comboBox)//установка значений 
        {
            if (comboBox.SelectedIndex != -1)
            {
                return comboBox.SelectedItem.ToString();
            }
            else { return "Неизвестно"; }
        }
        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)//обработчик нажатия на клавишу для начала расчета
        {
            if (ValidValue())
            {
                // Получаем значения из комбо-боксов
                string car = SetValue(comboBoxCar);
                string PointA = SetValue(comboBoxPointOne);
                string PointB = SetValue(comboBoxPointTwo);
                // Получаем выбранный тип топлива
                string fuelType = comboBoxFuelType.SelectedItem.ToString();
                // Получаем числовые значения из текстовых полей
                double dictance = Convert.ToDouble(this.textBoxDistance.Text);
                double fuelPrice = Convert.ToDouble(this.textBoxFuelPrice.Text);
                double consimption = Convert.ToDouble(this.textBoxConsumption.Text);
                double averageSpeed = Convert.ToDouble(this.textBoxAverSpeed.Text);
                // Проверяем условие и увеличиваем расход топлива, если средняя скорость превышает 140 км/ч
                if (averageSpeed > 140 && car == "Неизвестно")
                {
                    consimption += 1;
                }
                // Выполняем необходимые расчеты
                double usedFuel = Math.Round(dictance / consimption);
                double result = dictance / averageSpeed;
                double fullPrice = Math.Round((dictance / 100) * fuelPrice * consimption, 2);
                // Получаем выбранную дату
                DateTime dateOne = DataPickerFirstData.SelectedDate.Value;
                string date = dateOne.ToString("dd.MM.yyyy");
                // Выводим результаты в соответствующие текстовые поля
                this.textBoxUsedFuel.Text = usedFuel.ToString();
                this.textBoxTime.Text = Math.Round(result, 2).ToString();
                this.textBoxPrice.Text = fullPrice.ToString();
                // Создаем объект UsersRoutes и добавляем маршрут в XML
                UsersRoutes route = new UsersRoutes(UserName, car, PointA, PointB, dictance, fullPrice, fuelType, date, usedFuel, averageSpeed);
                if (route.AddRoutesInXML())
                {
                    // Предлагаем сохранить маршрут в docx файле
                    MessageBoxResult message = MessageBox.Show($"Поездка оформлена. Желаете сохранить данную поезку в docx файле?", "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (message == MessageBoxResult.OK)
                    {
                        CreateDocxFile(dateOne, car, PointA, PointB, dictance, fullPrice, fuelType, usedFuel, averageSpeed);
                    }
                }
                else
                {
                    MessageBox.Show("Поездка не оформлена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля корректными значениями", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateDocxFile(DateTime reportDate, string car, string PointA, string PointB, double dictance, double fullPrice, string fuelType, double usedFuel, double AverageSpeed)// создание Docx файла
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Документ Word (*.docx)|*.docx";
                saveFileDialog.FileName = $"Отчет о поездке на {reportDate:dd.MM.yyyy}.docx";
                if (saveFileDialog.ShowDialog() == true)
                {
                    string fileName = saveFileDialog.FileName;
                    // Создание документа Word
                    using (DocX document = DocX.Create(fileName))
                    {
                        string reportTitle = $"Отчет о поездке на {reportDate:dd.MM.yyyy}";
                        document.InsertParagraph(reportTitle).FontSize(12d).Bold().Alignment = Alignment.center;

                        // Добавление информации о средней стоимости, жидком топливе и электричестве
                        string tripDescription = $"Пользователь {UserName} {Surname} совершил поездку";
                        if (car != "Неизвестно")
                        {
                            tripDescription += $" на {car}";
                        }
                        if (PointA != "Неизвестно" && PointB != "Неизвестно")
                        {
                            tripDescription += $" из {PointA} - {PointB}";
                        }
                        tripDescription += $", дистанция маршрута составила {dictance}.";
                        document.InsertParagraph(tripDescription).FontSize(12d);
                        document.InsertParagraph($"Поездка обошлась в {fullPrice:0.00} рублей.").FontSize(12d);
                        if (fuelType == "Электричесво")
                        {
                            document.InsertParagraph($"Во время поездки было потрачено {fuelType} {usedFuel} Киловатт-часов.").FontSize(12d);
                        }
                        else
                        {
                            document.InsertParagraph($"Во время поездки было потрачено {fuelType} {usedFuel} Литров.").FontSize(12d);

                        }
                        document.InsertParagraph($"Средняя скорость поездки {AverageSpeed} Км/ч.").FontSize(12d);
                        document.InsertParagraph($"Дата формирования отчета {DateTime.Now:dd.MM.yyyy}").FontSize(12d).Alignment = Alignment.right;
                        // Сохранение документа
                        document.Save();
                    }
                    MessageBox.Show("Отчет успешно создан!", "Создание отчета", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show("Отчет не создан. Закройте открытый отчет и повторите попытку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void comboBoxFuelOctan_SelectionChanged(object sender, SelectionChangedEventArgs e)// проверка выбора в comboBoxFuelOctan
        {
            if (comboBoxFuelOctan.SelectedIndex != -1)
            {
                string selectedItem = comboBoxFuelOctan.SelectedItem.ToString();
                FuelPrice(selectedItem);
                textBoxFuelPrice.IsReadOnly = true;
            }
            else
            {
                textBoxFuelPrice.IsReadOnly = false;
            }
        }

        public void FuelPrice(string name)//вывод цены на топливо
        {
            foreach (var item in PriceList)
            {
                if (item.octaneNumber == name)
                {
                    textBoxFuelPrice.Text = item.price.ToString();
                    break;
                }
            }
        }
        public void ConsumptionCar(string text, string type)//выбор типа потребления топлива
        {
            foreach (var item in CarList)
            {
                if (item.Name == text)
                {
                    if (type == "Городской")
                    {
                        textBoxConsumption.Text = item.FuelConsumptionCity.ToString();
                        break;
                    }
                    else if (type == "По трассе")
                    {
                        textBoxConsumption.Text = item.FuelConsumptionHighway.ToString();
                        break;
                    }
                    else
                    {
                        textBoxConsumption.Text = item.FuelConsumptionGeneral.ToString();
                        break;
                    }
                }
            }
        }
        private void comboBoxConsumption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxConsumption.SelectedIndex != -1 && comboBoxCar.SelectedIndex != -1)
            {
                ConsumptionCar(comboBoxCar.SelectedItem.ToString(), comboBoxConsumption.SelectedItem.ToString());
                isMessageBoxShown = false;
            }
            else
            {
                if (!isMessageBoxShown)
                {
                    MessageBox.Show("Выберите автомобиль или не используйте данное поле", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    isMessageBoxShown = true;
                }
                comboBoxConsumption.SelectedIndex = -1;
            }
        }

        private void comboBoxCar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxCar.SelectedIndex != -1)
            {
                foreach (var item in CarList)
                {
                    if (item.Name == comboBoxCar.SelectedItem.ToString())
                    {
                        textBoxConsumption.Text = item.FuelConsumptionGeneral.ToString();
                        comboBoxConsumption.SelectedIndex = 2;
                        comboBoxFuelType.SelectedIndex = FuelList.IndexOf(item.Fuel);
                        comboBoxFuelOctan.SelectedIndex = comboBoxFuelOctan.Items.IndexOf(item.FuelOctan);
                        comboBoxFuelType.IsEnabled = false;
                    }
                }
                comboBoxConsumption.IsEnabled = true;
            }
            else
            {
                comboBoxFuelType.IsEnabled = true;
                comboBoxConsumption.IsEnabled = false;
                textBoxConsumption.Clear();
                textBoxFuelPrice.Clear();
                comboBoxConsumption.SelectedIndex = -1;
                comboBoxFuelType.SelectedIndex = -1;
                comboBoxFuelOctan.SelectedIndex = -1;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)//очистка всех полей 
        {
            isMessageBoxShown = true;
            textBoxDistance.Clear();
            textBoxAverSpeed.Clear();
            textBoxTime.Clear();
            textBoxConsumption.Clear();
            textBoxPrice.Clear();
            textBoxFuelPrice.Clear();
            textBoxUsedFuel.Clear();
            comboBoxFuelType.SelectedIndex = -1;
            comboBoxFuelOctan.SelectedIndex = -1;
            comboBoxCar.SelectedIndex = -1;
            comboBoxConsumption.SelectedIndex = -1;
            comboBoxPointOne.SelectedIndex = -1;
            comboBoxPointTwo.SelectedIndex = -1;

        }
        private static List<string> GetFuelList(string type) // получение названий топлива
        {
            List<string> Fuel = new List<string>();
            XmlDocument doc = new XmlDocument();
            doc.Load("Fuel.xml");
            XmlNodeList fuelNodes = doc.GetElementsByTagName("fuel");
            foreach (XmlNode fuelNode in fuelNodes)
            {
                XmlNode nameNode = fuelNode.SelectSingleNode("name");
                if (nameNode.InnerText == type)
                {
                    XmlNode octaneNode = fuelNode.SelectSingleNode("octaneNumber");
                    Fuel.Add(octaneNode.InnerText);
                }
            }
            return Fuel;
        }
        private void comboBoxPointOne_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        private void comboBoxPointTwo_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        private bool ValidValue()//проверка значений
        {
            double distance, averageSpeed, consumption, fuelPrice;
            if (IsValidDoubleInput(textBoxDistance, 0, 10000, out distance) &
                IsValidDoubleInput(textBoxAverSpeed, 0, 400, out averageSpeed) &
                IsValidDoubleInput(textBoxConsumption, 0, 100, out consumption) &
                IsValidDoubleInput(textBoxFuelPrice, 0, 1000, out fuelPrice) &
                IsValidDataInput(DataPickerFirstData))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsValidDoubleInput(TextBox box, int min, int max, out double value)//проверка корректности вещественного значения в textBox
        {
            if (box.Text == "")
            {
                value = 0;
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
        private bool IsValidDataInput(DatePicker picker) // проверка корректности даты
        {
            if (picker.SelectedDate.HasValue & picker.SelectedDate < DateTime.Now)
            {
                picker.BorderBrush = Brushes.Gray;
                return true;
            }
            else
            {
                picker.BorderBrush = Brushes.Red;
                return false;
            }
        }

        private string FixStr(string input)//замена . на ,
        {
            return input.Replace('.', ',');
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)//открытие окна с графиком 
        {
            GraphForm graphForm = new GraphForm(UserName, Surname);
            graphForm.ShowDialog();
        }

        private void comboBoxPointTwo_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)//очистка comboBox двойным нажатием
        {
            comboBoxPointTwo.SelectedIndex = -1;
        }

        private void comboBoxPointOne_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)//очистка comboBox двойным нажатием
        {
            comboBoxPointOne.SelectedIndex = -1;
        }

        private void comboBoxCar_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)//очистка comboBox двойным нажатием
        {
            comboBoxCar.SelectedIndex = -1;
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)//открытие справки 
        {
            Process.Start("hh.exe", "HelpBook.chm");
        }

        private void comboBoxFuelType_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)//очистка comboBox двойным нажатием
        {
            if (comboBoxCar.SelectedIndex != -1)
            {
                comboBoxFuelType.SelectedIndex = -1;
                comboBoxFuelOctan.SelectedIndex = -1;
            }
        }
    }
}

