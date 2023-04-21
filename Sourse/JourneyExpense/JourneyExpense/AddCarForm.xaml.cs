using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        public void InitComboBox()
        {
            comboBoxFuelType.ItemsSource = Fuel;
            comboBoxCarType.ItemsSource = CarType;
        }

        private void comboBoxFuelType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textBoxFuelConsumptionCity.Visibility = Visibility.Visible;
            textBoxFuelConsumptionHighway.Visibility = Visibility.Visible;
            comboBoxFuelType.IsReadOnly = false;
            textBoxSizeEngine.IsReadOnly = false;
            string selectedItem = comboBoxFuelType.SelectedItem.ToString();
            if (selectedItem == "Бензин")
            {
                comboBoxOctan.ItemsSource = new List<string> { "АИ-92", "АИ-95", "АИ-98" };
                comboBoxOctan.SelectedItem = "АИ-92";
                tank.Content = "Объем бака";
            }
            else if (selectedItem == "Дизельное топливо")
            {
                comboBoxOctan.ItemsSource = new List<string> { "ДТ" };
                comboBoxOctan.SelectedItem = "ДТ";
                comboBoxFuelType.IsReadOnly = true;
                tank.Content = "Объем бака";
            }
            else if (selectedItem == "Электричество")
            {
                comboBoxOctan.ItemsSource = null;
                comboBoxOctan.SelectedItem = null;
                textBoxFuelConsumptionCity.Visibility = Visibility.Hidden;
                textBoxFuelConsumptionHighway.Visibility = Visibility.Hidden;
                tank.Content = "Объем батареи";
                textBoxFuelConsumptionCity.Clear();
                textBoxFuelConsumptionHighway.Clear();
                textBoxSizeEngine.IsReadOnly = true;
            }
        }
        private bool ValidValue()
        {
            if (TextBoxValid(textBoxName) & ValidFuel() & СonsumptionСheck() & LogicCheck())
            {
                return true;
            }
            return false;
        }
        /*private bool LogicCheck()
        {
            string selectedItem = comboBoxCarType.SelectedItem.ToString();
            int year, seatingCapacity,maxSpeed;
            bool isNumericOne = int.TryParse(textBoxYear.Text, out year);
            bool isNumericTwo = int.TryParse(textBoxPlace.Text, out seatingCapacity);
            bool isNumericThree = int.TryParse(textBoxMaxSpeed.Text, out maxSpeed);
            if (isNumericOne && isNumericTwo && isNumericThree)
            {
                if(maxSpeed < 0  && maxSpeed > 500)
                {
                    return false;
                }    
                if (year < 1950)
                {
                    return false;
                }
                if (selectedItem == "Седан" || selectedItem == "Купе" || selectedItem == "Кабриолет" || selectedItem == "Лифтбек" || selectedItem == "Фургон" || selectedItem == "Пикап")
                {
                    if (selectedItem == "Фургон")
                    {
                        if (seatingCapacity >= 2 && seatingCapacity <= 9)
                        {

                            return true;
                        }
                        else 
                        {
                            MessageBox.Show("Ошибка Фургон");
                            return false;
                        }
                    }
                    if (seatingCapacity >= 2 && seatingCapacity <= 5)
                    {
                        return true;
                    }
                    else 
                    {
                        MessageBox.Show("Ошибка 2 5");
                        return false;
                    }
                }
                else if (selectedItem == "Универсал" || selectedItem == "Внедорожник" || selectedItem == "Кроссовер" || selectedItem == "Минивэн")
                {
                    if (seatingCapacity >= 5 && seatingCapacity <= 8)
                    {
                        return true;
                    }
                    else 
                    {
                        MessageBox.Show("Ошибка 5 8");
                        return false; 
                    }
                }
            }
            return false;
        }*/
        private bool LogicCheck()
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
        private bool СonsumptionСheck()
        {
            string selectedItem = IsValidComboBox(comboBoxFuelType);
            double ConsumptionGeneral, ConsumptionCity, ConsumptionHighway, enginePower, engineSize, tankSize;
            bool isNumericOne = IsValidDoubleInput(textBoxFuelConsumptionGeneral, 0, 100, out ConsumptionGeneral);
            bool isNumericTwo = IsValidDoubleInput(textBoxFuelConsumptionCity, 0, 100, out ConsumptionCity);
            bool isNumericThree = IsValidDoubleInput(textBoxFuelConsumptionHighway, 0, 100, out ConsumptionHighway);
            bool isNumericFour = IsValidDoubleInput(textBoxPower, 0, 100, out enginePower);
            bool isNumericFive = IsValidDoubleInput(textBoxSizeEngine, 0, 9, out engineSize);
            bool isNumericSix = IsValidDoubleInput(textBoxSizeTank, 0, 600, out tankSize);
            if (selectedItem == "Бензин" || selectedItem == "Дизельное топливо")
            {
                if (isNumericOne && isNumericTwo && isNumericThree && isNumericFour && isNumericFive && isNumericSix)
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
        private bool ValidFuel()
        {
            string selectedItem = IsValidComboBox(comboBoxFuelType);
            if (selectedItem == "Бензин" || selectedItem == "Дизельное топливо")
            {
                if (comboBoxFuelType.SelectedIndex != -1 && textBoxFuelConsumptionGeneral.Text != "" && textBoxFuelConsumptionCity.Text != "" && textBoxFuelConsumptionHighway.Text != "" && textBoxSizeEngine.Text != "")
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidValue())
            {
                string name = this.textBoxName.Text;
                int year = Convert.ToInt32(this.textBoxYear.Text);
                string typeCar = this.comboBoxCarType.SelectedItem.ToString();
                int place = Convert.ToInt32(this.textBoxPlace.Text);
                int maxSpeed = Convert.ToInt32(this.textBoxMaxSpeed.Text);
                string typeFuel = this.comboBoxFuelType.SelectedItem.ToString();
                string typeOctan = this.comboBoxOctan.SelectedItem.ToString();
                double fuelConsumptionGeneral = DoubleNull(textBoxFuelConsumptionGeneral);
                double fuelConsumptionCity = DoubleNull(textBoxFuelConsumptionCity);
                double fuelConsumptionHighway = DoubleNull(textBoxFuelConsumptionHighway);
                double enginePower = Convert.ToDouble(FixStr(this.textBoxPower.Text));
                double engineSize = Convert.ToDouble(FixStr(this.textBoxSizeEngine.Text));
                double tankSize = Convert.ToDouble(FixStr(this.textBoxSizeTank.Text));
                Car car = new Car(name, year, typeCar, place, maxSpeed, typeFuel, typeOctan, fuelConsumptionGeneral, fuelConsumptionCity, fuelConsumptionHighway, enginePower, engineSize, tankSize);
                car.AddCarInXML();
                MessageBox.Show($"Автомобиль: {name} был добавлен в систему");
            }
            else
            {
                MessageBox.Show("Некорректные данные");
            }
        }
        private double DoubleNull(TextBox text)
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
        private string FixStr(string input)
        {
            return input.Replace('.', ',');
        }
        private bool SetValueComboBox(ComboBox combo, Label label)//to do
        {
            if (comboBoxCarType.SelectedIndex == -1)
            {
                MessageBox.Show($"Не выбрано поле в {LabelTypeCar.Content}");
                combo.BorderBrush = Brushes.Red;
                return false;
            }
            else
            {
                combo.BorderBrush = Brushes.Gray;
                return true;
            }
        }
        private bool IsValidIntInput(TextBox text, int min, int max, out int value)
        {
            if (text.Text == "")
            {
                value = 0;
                text.BorderBrush = Brushes.Red;
                //text.ToolTip = $"Введите значение в диапазоне от {min} - {max}";
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
                    //text.ToolTip = $"Введите значение в диапазоне от {min} - {max}";
                    return false;
                }
            }
            else
            {
                text.BorderBrush = Brushes.Red;
                return false;
            }
        }
        private bool IsValidDoubleInput(TextBox box, int min, int max, out double value)
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
        private string IsValidComboBox(ComboBox box)
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
        private bool TextBoxValid(TextBox text)
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
