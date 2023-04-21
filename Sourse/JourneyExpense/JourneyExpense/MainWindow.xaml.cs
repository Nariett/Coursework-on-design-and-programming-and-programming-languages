using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace JourneyExpense
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitComboBox();

        }
        private List<string> Fuel = new List<string>() { "Бензин", "Дизельное топливо", "Электричество" };
        private List<string> TypeConsuption = new List<string> { "Городской", "По трассе", "Смешанный" };
        public void InitRoutes()
        {

        }
        public void InitComboBox()
        {
            foreach (var item in Fuel)
            {
                comboBoxFuelType.Items.Add(item);
            }
            foreach (var item in TypeConsuption)
            {
                comboBoxConsumption.Items.Add(item);
            }
        }

        private void comboBoxFuelType_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            comboBoxFuelOctan.Visibility = Visibility.Visible;
            comboBoxFuelOctan.IsReadOnly = false;
            string selectedItem = comboBoxFuelType.SelectedItem.ToString();
            if (selectedItem == "Бензин")
            {
                comboBoxFuelOctan.ItemsSource = new List<string> { "АИ-92", "АИ-95", "АИ-98" };
                comboBoxFuelOctan.SelectedItem = "АИ-92";
                LabelConsumption.Content = "Литр";
            }
            else if (selectedItem == "Дизельное топливо")
            {
                comboBoxFuelOctan.ItemsSource = new List<string> { "ДТ" };
                comboBoxFuelOctan.SelectedItem = "ДТ";
                comboBoxFuelType.IsReadOnly = true;
                LabelConsumption.Content = "Литр";
            }
            else if (selectedItem == "Электричество")
            {
                comboBoxFuelOctan.Visibility = Visibility.Hidden;
                LabelConsumption.Content = "Вт-ч";
                comboBoxFuelOctan.ItemsSource = null;
                comboBoxFuelOctan.SelectedItem = null;
                comboBoxFuelOctan.IsReadOnly = true;
            }
        }

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double dictance = Convert.ToDouble(this.textBoxDistance.Text);
                double averageSpeed = Convert.ToDouble(this.textBoxAverSpeed.Text);
                double result = dictance / averageSpeed;
                this.textBoxTime.Text = result.ToString();
            }
            catch(Exception ex )
            {
                MessageBox.Show("Заполните все поля корректными значениями");
            }
        }
    }
}
