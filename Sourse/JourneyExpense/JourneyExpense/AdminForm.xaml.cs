using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JourneyExpense
{
    /// <summary>
    /// Логика взаимодействия для AdminForm.xaml
    /// </summary>
    public partial class AdminForm : Window
    {
        public AdminForm()
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
            if (textBoxName.Text != "" && textBoxYear.Text != "" && textBoxMaxSpeed.Text != "" && /*SetValueComboBox(comboBoxCarType,LabelTypeCar)*/comboBoxCarType.SelectedIndex != -1 && textBoxPlace.Text != "" && textBoxPower.Text != "" && textBoxSizeTank.Text != "" && comboBoxFuelType.SelectedIndex != -1 && ValidFuel())
            {
                if (СonsumptionСheck() && LogicCheck())
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
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
            string? selectedItem = comboBoxCarType.SelectedItem.ToString();
            int year, speed, place,power, minSeats = 2, maxSeats = 5;
            bool IsYear, IsSpeed, IsSeats;
            IsYear = SetIntTextBox(textBoxYear, 1980, 2024, out year);
            IsSpeed = SetIntTextBox(textBoxMaxSpeed, 0, 500, out speed);
            IsSeats = SetIntTextBox(textBoxPlace, 0, 11, out place);
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
                if (IsYear && IsSpeed)
                {
                    comboBoxCarType.BorderBrush = Brushes.Gray;
                    return true;
                }
                MessageBox.Show("Требуется целочисленное число ");
                return false;
            }
            else
            {
                MessageBox.Show($"Ошибка количества мест. Требуется от {minSeats} до {maxSeats} мест для автомобиля типа {selectedItem}");
                textBoxPlace.BorderBrush = Brushes.Red;
                return false;
            }

        }
        private bool СonsumptionСheck()
        {
            string selectedItem = comboBoxFuelType.SelectedItem.ToString();
            double ConsumptionGeneral, ConsumptionCity, ConsumptionHighway, enginePower, engineSize, tankSize;
            bool isNumericOne = double.TryParse(FixStr(textBoxFuelConsumptionGeneral.Text), out ConsumptionGeneral);
            bool isNumericTwo = double.TryParse(FixStr(textBoxFuelConsumptionCity.Text), out ConsumptionCity);
            bool isNumericThree = double.TryParse(FixStr(textBoxFuelConsumptionHighway.Text), out ConsumptionHighway);
            bool isNumericFour = double.TryParse(FixStr(textBoxPower.Text), out enginePower);
            bool isNumericFive = double.TryParse(FixStr(textBoxSizeEngine.Text), out engineSize);
            bool isNumericSix = double.TryParse(FixStr(textBoxSizeTank.Text), out tankSize);


            if (selectedItem == "Бензин" || selectedItem == "Дизельное топливо")
            {
                if (isNumericOne && isNumericTwo && isNumericThree && isNumericFour && isNumericFive && isNumericSix)
                {
                    if ((ConsumptionGeneral > 0 && ConsumptionGeneral < 100) && (ConsumptionCity > 0 && ConsumptionCity < 100) && (ConsumptionHighway > 0 && ConsumptionHighway < 100) && enginePower > 0 && enginePower < 500 && engineSize > 0 && tankSize > 0)
                    {
                        //MessageBox.Show("Потребление в норме");
                        textBoxFuelConsumptionGeneral.BorderBrush = Brushes.Gray;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка 1");
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (selectedItem == "Электричество")
            {
                if (isNumericOne && isNumericFour && isNumericSix)
                {
                    if ((ConsumptionGeneral > 0 && ConsumptionGeneral < 100) && enginePower > 0 && enginePower < 500 && tankSize > 0)
                    {
                        //MessageBox.Show("Потребление в норме");
                        textBoxFuelConsumptionGeneral.BorderBrush = Brushes.Gray;
                        return true;
                    }
                    else
                    {
                        textBoxFuelConsumptionGeneral.BorderBrush = Brushes.Red;
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        private bool ValidFuel()
        {
            string selectedItem = comboBoxFuelType.SelectedItem.ToString();
            if (selectedItem == "Бензин" || selectedItem == "Дизельное топливо")
            {
                if (comboBoxFuelType.SelectedIndex != -1 && textBoxFuelConsumptionGeneral.Text != "" && textBoxFuelConsumptionCity.Text != "" && textBoxFuelConsumptionHighway.Text != "" && textBoxSizeEngine.Text != "")
                {
                    return true;
                }
                else { return false; }
            }
            else
            {
                if (comboBoxFuelType.SelectedIndex != -1 && textBoxFuelConsumptionGeneral.Text != "")
                {
                    return true;
                }
                else { return false; }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ValidValue())
            {
                MessageBox.Show("Круто чувак");
            }
            else
            {
                MessageBox.Show("Плохо чувак");
            }
        }
        private string FixStr(string input)
        {
            return input.Replace('.', ',');
        }
        private bool SetValueComboBox(ComboBox combo,Label label)
        {
            if(comboBoxCarType.SelectedIndex == -1)
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
        private bool SetIntTextBox(TextBox text, int min, int max, out int value)
        {
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
    }
}

