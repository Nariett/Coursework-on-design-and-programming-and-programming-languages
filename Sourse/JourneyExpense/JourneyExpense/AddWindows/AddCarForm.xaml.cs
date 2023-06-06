using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using JourneyExpense.Classes;

namespace JourneyExpense
{
    /// <summary>
    /// Логика взаимодействия для AddCarForm.xaml
    /// </summary>
    public partial class AddCarForm : Window
    {
        public AddCarForm()
        {
            InitializeComponent();
            InitComboBox();
        }
        private List<string> Fuel = new List<string>() { "Бензин", "Дизельное топливо", "Электричество" };
        private List<string> CarType = new List<string>() { "Седан", "Купе", "Хэтчбек", "Универсал", "Внедорожник", "Кроссовер", "Кабриолет", "Лифтбек", "Фургон", "Минивэн", "Пикап" };

        public void InitComboBox()// иницилизцаии comboBox
        {
            comboBoxFuelType.ItemsSource = Fuel;
            comboBoxCarType.ItemsSource = CarType;
        }
        private static List<string> GetFuelList(string type)//функция для получения списка топлива
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
        private void comboBoxFuelType_SelectionChanged(object sender, SelectionChangedEventArgs e) // обработчик изменение значения в comboBox
        {
            comboBoxOctan.Visibility = Visibility.Visible;
            OctanLabel.Visibility = Visibility.Visible;
            textBoxFuelConsumptionCity.Visibility = Visibility.Visible;
            textBoxFuelConsumptionHighway.Visibility = Visibility.Visible;
            comboBoxFuelType.IsReadOnly = false;
            string selectedItem = comboBoxFuelType.SelectedItem.ToString();
            if (selectedItem == "Бензин")
            {
                comboBoxOctan.ItemsSource = GetFuelList("Бензин");
                tank.Content = "Объем бака";
            }
            else if (selectedItem == "Дизельное топливо")
            {
                comboBoxOctan.ItemsSource = GetFuelList("Дизельное топливо");
                comboBoxFuelType.IsReadOnly = true;
                tank.Content = "Объем бака";
            }
            else if (selectedItem == "Электричество")
            {
                comboBoxOctan.ItemsSource = null;
                comboBoxOctan.SelectedItem = null;
                comboBoxOctan.Visibility = Visibility.Hidden;
                OctanLabel.Visibility = Visibility.Hidden;
                textBoxFuelConsumptionCity.Visibility = Visibility.Hidden;
                textBoxFuelConsumptionHighway.Visibility = Visibility.Hidden;
                textBoxFuelConsumptionCity.Clear();
                textBoxFuelConsumptionHighway.Clear();
                tank.Content = "Объем батареи";
            }
            comboBoxOctan.SelectedIndex = 1;
        }
        private bool ValidValue() // проверка валидности значений
        {
            if (TextBoxValid(textBoxName) & ValidFuel() & СonsumptionСheck() & LogicCheck())
            {
                return true;
            }
            return false;
        }
        private bool LogicCheck() // логическая проверка значений в полях 
        {
            string selectedItem = IsValidComboBox(comboBoxCarType);
            int year, speed, place, minSeats = 2, maxSeats = 5;
            bool IsYear, IsSpeed, IsSeats;
            IsYear = IsValidIntInput(textBoxYear, 1980, 2024, out year);
            IsSpeed = IsValidIntInput(textBoxMaxSpeed, 0, 500, out speed);
            IsSeats = IsValidIntInput(textBoxPlace, 0, 11, out place);
            if (selectedItem == "Фургон")
            {
                minSeats = 2;
                maxSeats = 10;
            }
            else if (selectedItem == "Универсал" || selectedItem == "Внедорожник" || selectedItem == "Кроссовер" || selectedItem == "Минивэн")
            {
                minSeats = 5;
                maxSeats = 8;
            }
            if (place >= minSeats && place <= maxSeats)
            {
                textBoxPlace.BorderBrush = Brushes.Gray;
                if (IsYear && IsSpeed && IsSeats)
                {
                    comboBoxCarType.BorderBrush = Brushes.Gray;
                    return true;
                }
                return false;
            }
            else
            {
                if (selectedItem == "Ошибка")
                {
                    textBoxPlace.BorderBrush = Brushes.Red;
                    return false;
                }
                MessageBox.Show($"Ошибка количества мест. Требуется от {minSeats} до {maxSeats} мест для автомобиля типа {selectedItem}");
                textBoxPlace.BorderBrush = Brushes.Red;
                return false;
            }

        }
        private bool СonsumptionСheck() // проверка потребления
        {
            string selectedItem = IsValidComboBox(comboBoxFuelType);
            double ConsumptionGeneral, ConsumptionCity, ConsumptionHighway, enginePower, engineSize, tankSize;
            bool isNumericOne = IsValidDoubleInput(textBoxFuelConsumptionGeneral, 0, 100, out ConsumptionGeneral);
            bool isNumericTwo = IsValidDoubleInput(textBoxFuelConsumptionCity, 0, 100, out ConsumptionCity);
            bool isNumericThree = IsValidDoubleInput(textBoxFuelConsumptionHighway, 0, 100, out ConsumptionHighway);
            bool isNumericFour = IsValidDoubleInput(textBoxPower, 0, 2000, out enginePower);
            bool isNumericSix = IsValidDoubleInput(textBoxSizeTank, 0, 600, out tankSize);
            if (selectedItem == "Бензин" || selectedItem == "Дизельное топливо")
            {
                if (isNumericOne && isNumericTwo && isNumericThree && isNumericFour && isNumericSix)
                {
                    textBoxFuelConsumptionGeneral.BorderBrush = Brushes.Gray;
                    return true;
                }
            }
            else if (selectedItem == "Электричество")
            {
                if (isNumericOne && isNumericFour && isNumericSix)
                {
                    textBoxFuelConsumptionGeneral.BorderBrush = Brushes.Gray;
                    return true;
                }
                textBoxFuelConsumptionGeneral.BorderBrush = Brushes.Red;
            }
            return false;
        }
        private bool ValidFuel() // проверка корректности выбора топлива
        {
            string selectedItem = IsValidComboBox(comboBoxFuelType);
            if (selectedItem == "Бензин" || selectedItem == "Дизельное топливо")
            {
                if (comboBoxFuelType.SelectedIndex != -1 && textBoxFuelConsumptionGeneral.Text != "" && textBoxFuelConsumptionCity.Text != "" && textBoxFuelConsumptionHighway.Text != "")
                {
                    return true;
                }
            }
            else
            {
                if (comboBoxFuelType.SelectedIndex != -1 && textBoxFuelConsumptionGeneral.Text != "")
                {
                    return true;
                }
            }
            return false;
        }

        private void Button_Click(object sender, RoutedEventArgs e) // обработчик нажатия на кнопку
        {
            if (ValidValue())
            {
                string name = this.textBoxName.Text;
                int year = Convert.ToInt32(this.textBoxYear.Text);
                string typeCar = this.comboBoxCarType.SelectedItem.ToString();
                int place = Convert.ToInt32(this.textBoxPlace.Text);
                int maxSpeed = Convert.ToInt32(this.textBoxMaxSpeed.Text);
                string typeFuel = this.comboBoxFuelType.SelectedItem.ToString();
                double fuelConsumptionGeneral = DoubleNull(textBoxFuelConsumptionGeneral);
                double fuelConsumptionCity = DoubleNull(textBoxFuelConsumptionCity);
                double fuelConsumptionHighway = DoubleNull(textBoxFuelConsumptionHighway);
                double enginePower = Convert.ToDouble(FixStr(this.textBoxPower.Text));
                double tankSize = Convert.ToDouble(FixStr(this.textBoxSizeTank.Text));
                Car car;
                if (typeFuel == "Электричество")
                {
                    car = new Car(name, year, typeCar, maxSpeed, place, typeFuel, "Медл.", fuelConsumptionGeneral, fuelConsumptionGeneral, fuelConsumptionGeneral, enginePower, tankSize);
                }
                else
                {
                    string typeOctan = comboBoxOctan.SelectedItem.ToString();
                    car = new Car(name, year, typeCar,  maxSpeed, place, typeFuel, typeOctan, fuelConsumptionGeneral, fuelConsumptionCity, fuelConsumptionHighway, enginePower, tankSize);
                }
                if (car.AddCarInXML())
                {
                    MessageBox.Show($"Автомобиль: {name} был добавлен в систему.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Автомобиль: {name} не был добавлен в систему. Автомобиль уже находится в системе.","Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Некорректные данные","Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private double DoubleNull(TextBox text) // проверка пустоты текстовых полей 
        {
            if (text.Text != "")
            {
                return Convert.ToDouble(FixStr(text.Text));
            }
            else
            {
                return 0;
            }
        }
        private string FixStr(string input) // замена точки на запятую
        {
            return input.Replace('.', ',');
        }
        private bool IsValidIntInput(TextBox text, int min, int max, out int value) // проверка корректности введенных целочисленных значений 
        {
            if (text.Text == "")
            {
                value = 0;
                text.BorderBrush = Brushes.Red; // смена цвета границы элемента
                return false;
            }
            bool isNumeric = int.TryParse(text.Text, out value);
            if (isNumeric)
            {
                if (value > min && value < max)
                {
                    text.BorderBrush = Brushes.Gray;
                    return true;
                }
                else
                {
                    text.BorderBrush = Brushes.Red;
                    return false;
                }
            }
            else
            {
                text.BorderBrush = Brushes.Red;
                return false;
            }
        }
        private bool IsValidDoubleInput(TextBox box, int min, int max, out double value)// проверка корректности введенных вещественное  значений 
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
        private string IsValidComboBox(ComboBox box)// проверка корректности выбора значения в comboBox
        {
            if (box.SelectedIndex == -1)
            {
                return "Ошибка";
            }
            else
            {
                return box.SelectedItem.ToString();
            }
        }
        private bool TextBoxValid(TextBox text)// проверка значения в textBox 
        {
            if (text.Text == "")
            {
                text.BorderBrush = Brushes.Red;
                return false;
            }
            else
            {
                text.BorderBrush = Brushes.Gray;
                return true;
            }
        }
    }
}
