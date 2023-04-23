using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Dynamic;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

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
            InitList();
            InitComboBox();
        }
        private bool isMessageBoxShown = false;
        private List<string> FuelList = new List<string>() { "Бензин", "Дизельное топливо", "Электричество" };
        private List<string> TypeConsuption = new List<string> { "Городской", "По трассе", "Смешанный" };
        private List<Car> CarList = new List<Car>();
        private List<Fuel> PriceList = new List<Fuel>();
        public void InitList()
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
        }
        public void InitComboBox()
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
        }

        private void comboBoxFuelType_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            string selectedItem = comboBoxFuelType.SelectedItem.ToString();
            if (selectedItem == "Бензин")
            {
                comboBoxFuelOctan.ToolTip = "Октановое число";
                comboBoxFuelOctan.ItemsSource = new List<string> { "АИ-92", "АИ-95", "АИ-98" };
                comboBoxFuelOctan.SelectedItem = "АИ-92";
                LabelConsumption.Content = "Литр";
            }
            else if (selectedItem == "Дизельное топливо")
            {
                comboBoxFuelOctan.ToolTip = "Октановое число";
                comboBoxFuelOctan.ItemsSource = new List<string> { "ДТ", "ДТ-ЭКО" };
                comboBoxFuelOctan.SelectedItem = "ДТ";
                LabelConsumption.Content = "Литр";
            }
            else if (selectedItem == "Электричество")
            {
                comboBoxFuelOctan.ToolTip = "Вид зарядки";
                comboBoxFuelOctan.ItemsSource = new List<string> { "Быстр.", "Медл." };
                comboBoxFuelOctan.SelectedItem = "Медл.";
                LabelConsumption.Content = "Вт-ч";
            }
        }

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double dictance = Convert.ToDouble(this.textBoxDistance.Text);
                double averageSpeed = Convert.ToDouble(this.textBoxAverSpeed.Text);
                double result = dictance / averageSpeed;
                this.textBoxTime.Text = Math.Round(result, 2).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Заполните все поля корректными значениями");
            }
        }

        private void comboBoxFuelOctan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxFuelOctan.SelectedIndex != -1)
            {
                string selectedItem = comboBoxFuelOctan.SelectedItem.ToString();
                if (selectedItem == "АИ-92")
                {
                    fuelPrice("АИ-92");
                }
                else if (selectedItem == "АИ-95")
                {
                    fuelPrice("АИ-95");
                }
                else if (selectedItem == "АИ-98")
                {
                    fuelPrice("АИ-98");
                }
                else if (selectedItem == "ДТ")
                {
                    fuelPrice("ДТ");
                }
                else if (selectedItem == "ДТ-ЭКО")
                {
                    fuelPrice("ДТ-ЭКО");
                }
                else if (selectedItem == "Быстр.")
                {
                    fuelPrice("Быстр.");
                }
                else if (selectedItem == "Медл.")
                {
                    fuelPrice("Медл.");
                }
            }
        }
        public void fuelPrice(string name)
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
        public void consumptionCar(string text, string type)
        {
            foreach(var item in CarList)
            {
                if(item.Name == text)
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
                consumptionCar(comboBoxCar.SelectedItem.ToString(), comboBoxConsumption.SelectedItem.ToString());
                isMessageBoxShown = false;
            }
            else
            {
                if (!isMessageBoxShown)
                {
                    MessageBox.Show("Выберите автомобиль и повторите повторно или не используйте данное поле");
                    isMessageBoxShown = true;
                }
                comboBoxConsumption.SelectedIndex = -1;
            }
        }
        private void comboBoxCar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach(var item in CarList)
            {
                if(item.Name == comboBoxCar.SelectedItem.ToString())
                {
                    textBoxConsumption.Text = item.FuelConsumptionGeneral.ToString();
                    comboBoxConsumption.SelectedIndex = 2;
                    comboBoxFuelType.SelectedIndex = FuelList.IndexOf(item.Fuel);
                    comboBoxFuelOctan.SelectedIndex = comboBoxFuelOctan.Items.IndexOf(item.FuelOctan);//проверить
                }
            }
        }
    }
}
