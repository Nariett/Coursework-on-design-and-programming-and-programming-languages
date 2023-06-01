using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using JourneyExpense.Classes;

namespace JourneyExpense
{
    /// <summary>
    /// Логика взаимодействия для AddFuelForm.xaml
    /// </summary>
    public partial class AddFuelForm : Window
    {
        public AddFuelForm()
        {
            InitializeComponent();
            InitComboBox();
        }
        private List<string> FuelType = new List<string>() { "Бензин", "Дизельное топливо", "Электричество" };
        public void InitComboBox()
        {
            foreach (var item in FuelType)
            {
                comboBoxFuelType.Items.Add(item);
            }
        }
        private void UpdatePriceButton_Click(object sender, RoutedEventArgs e)
        {
            double price = 0;
            if (comboBoxFuelType.SelectedIndex != -1 & TextBoxValid(textBoxFuelOctan) & IsValidDoubleInput(textBoxPrice, 0, 100, out price))
            {
                string name = comboBoxFuelType.SelectedItem.ToString();
                string octaneNumber = textBoxFuelOctan.Text.TrimEnd();
                Fuel fuel = new Fuel(name, octaneNumber.ToUpper(), price);
                if (fuel.AddFuelInXML())
                {
                    MessageBox.Show($"Топливо добавлено в систему.","Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Топливо уже находится в системе.","Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите корректные данные","Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
        private string FixStr(string input)
        {
            return input.Replace('.', ',');
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
