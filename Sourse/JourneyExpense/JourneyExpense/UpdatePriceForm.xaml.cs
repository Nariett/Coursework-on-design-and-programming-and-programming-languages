using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace JourneyExpense
{
    /// <summary>
    /// Логика взаимодействия для UpdatePriceForm.xaml
    /// </summary>
    public partial class UpdatePriceForm : Window
    {
        public UpdatePriceForm()
        {
            InitializeComponent();
            InitComboBox();
        }
        private List<string> FuelType = new List<string>() { "Бензин", "Дизельное топливо", "Электричество" };
        private List<Fuel> AllFuel = new Fuel().ReadFuelInXML();
        public void InitComboBox()
        {
            foreach (var item in FuelType)
            {
                comboBoxFuelType.Items.Add(item);
            }
        }
        private void UpdatePriceButton_Click(object sender, RoutedEventArgs e)
        {
            double value = 0;
            if (comboBoxFuelOctan.SelectedIndex != -1 && comboBoxFuelType.SelectedIndex != -1 && IsValidDoubleInput(textBoxPrice, 0, 100,out value))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("Fuel.xml");
                XmlNode fuelNode = xmlDoc.SelectSingleNode($"//fuel[name='{comboBoxFuelType.SelectedItem.ToString()}'][octaneNumber='{comboBoxFuelOctan.SelectedItem.ToString()}']");
                XmlNode priceNode = fuelNode.SelectSingleNode("price");
                priceNode.InnerText = FixStr(textBoxPrice.Text);
                xmlDoc.Save("Fuel.xml");
                MessageBox.Show($"Цена на {comboBoxFuelOctan.SelectedItem.ToString} изменена на {textBoxPrice.Text}"); 
            }
            else
            {
                MessageBox.Show("Ошибка ввода данных. Повторите попытку");
            }
        }

        private void comboBoxFuelType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxFuelType.SelectedIndex != -1)
            {
                comboBoxFuelOctan.Items.Clear();
                foreach (var item in AllFuel)
                {
                    if (item.name == comboBoxFuelType.SelectedItem.ToString())
                    {
                        comboBoxFuelOctan.Items.Add(item.octaneNumber);
                    }
                }
            }
        }

        private void comboBoxFuelOctan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxFuelOctan.SelectedIndex != -1)
            {
                foreach (var item in AllFuel)
                {
                    if (comboBoxFuelOctan.SelectedItem.ToString() == item.octaneNumber)
                    {
                        textBoxPrice.Text = item.price.ToString();
                    }
                }
            }
        }
        private string FixStr(string input)
        {
            return input.Replace('.', ',');/////////////////////////////////
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
    }
}
