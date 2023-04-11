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
            initComboBox();
        }
        public List<string> carBodyClasses = new List<string>{ "Седан", "Хэтчбек", "Универсал", "Купе", "Кабриолет", "Внедорожник", "Кроссовер", "Минивэн" };
        public static Dictionary<string, string> TypeFuel = new Dictionary<string, string>()
        {
            {"АИ-92","AI92"},
            {"АИ-95","AI95"},
            {"АИ-98","AI98"},
            {"ДТ","DT"},
            {"Электричество","Electric"}
        };
        public static List<string> Fuel = new List<string>();
        public static List<string> TypeConsuption = new List<string> { "Городской", "По трассе", "Смешанный" };
        public void initComboBox()
        {
            foreach (var pair in TypeFuel)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = pair.Key;
                comboBoxFuelType.Items.Add(item);
            }
            foreach (var item in TypeConsuption)
            {
                comboBoxConsumption.Items.Add(item);
            }
        }
        public void initDictionary()
        {

        }

        private void comboBoxFuelType_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectItem = comboBoxFuelType.SelectedItem as ComboBoxItem;
            string item = selectItem.Content.ToString();
            if (item == "Электричество")
            {
                LabelConsumption.Content = "Вт-ч";
            }else { LabelConsumption.Content = "Литр"; }

        }

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            double dictance = Convert.ToDouble(this.textBoxDistance.Text);
            double averageSpeed = Convert.ToDouble(this.textBoxAverSpeed.Text);
            double result = dictance / averageSpeed;
            this.textBoxTime.Text = result.ToString();
        }
    }
}
