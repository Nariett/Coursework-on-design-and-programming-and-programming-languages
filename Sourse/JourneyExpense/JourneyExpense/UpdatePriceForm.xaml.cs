using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
            foreach(var item in FuelType)
            {
                comboBoxFuelType.Items.Add(item);
            }
        }
        private void UpdatePriceButton_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Fuel.xml");

            // Находим узел с бензином АИ-95 и сохраняем его в переменную fuelNode
            XmlNode fuelNode = xmlDoc.SelectSingleNode($"//Fuel[name={comboBoxFuelType.SelectedItem.ToString()}][octaneNumber={comboBoxFuelOctan.SelectedItem.ToString()}]");

            // Находим узел с ценой внутри узла fuelNode и обновляем его значение
            XmlNode priceNode = fuelNode.SelectSingleNode("price");
            priceNode.InnerText = textBoxPrice.Text;

            // Сохраняем изменения в XML-файле
            xmlDoc.Save("Fuel.xml");
        }

        private void comboBoxFuelType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxFuelType.SelectedIndex != -1)
            {
                comboBoxFuelOctan.Items.Clear();
                foreach(var item in AllFuel)
                {
                    if(item.name == comboBoxFuelType.SelectedItem.ToString())
                    {
                        comboBoxFuelOctan.Items.Add(item.octaneNumber);
                    }
                }
            }
            else
            {

            }
        }

        private void comboBoxFuelOctan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*if (comboBox.SelectedIndex != -1)
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
            else
            {

            }*/
        }
    }
}
