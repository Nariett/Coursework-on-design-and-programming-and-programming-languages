using System;
using System.Collections.Generic;
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
        private List<string> PointA = new List<string>();
        private List<string> PointB = new List<string>();

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
            List<Route> AllRoute = Route.ReadRousteInXML();
            foreach(var item in AllRoute)
            {
                PointA.Add(item.PointA);
                PointB.Add(item.PointB);
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

        private void comboBoxFuelType_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxFuelType.SelectedIndex != -1)
            {
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
                }
                comboBoxFuelOctan.SelectedIndex = 1;
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
                this.textBoxPrice.Text = Math.Round(Convert.ToDouble(this.textBoxConsumption.Text) * Convert.ToDouble(this.textBoxFuelPrice.Text),2).ToString();
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
                FuelPrice(selectedItem);
            }
        }
        public void FuelPrice(string name)
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
        public void ConsumptionCar(string text, string type)
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
                    MessageBox.Show("Выберите автомобиль и повторите повторно или не используйте данное поле");
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
                        comboBoxFuelOctan.SelectedIndex = comboBoxFuelOctan.Items.IndexOf(item.FuelOctan);//проверить
                        comboBoxFuelType.IsEnabled = false;
                    }
                }
                comboBoxConsumption.IsEnabled = true;
            }
            else
            {
                comboBoxFuelType.IsEnabled = true;
                comboBoxConsumption.IsEnabled = false;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            isMessageBoxShown = true;
            textBoxDistance.Clear();
            textBoxAverSpeed.Clear();
            textBoxTime.Clear();
            textBoxConsumption.Clear();
            textBoxPrice.Clear();
            textBoxFuelPrice.Clear();
            comboBoxFuelType.SelectedIndex = -1;
            comboBoxFuelOctan.SelectedIndex = -1;
            comboBoxCar.SelectedIndex = -1;
            comboBoxConsumption.SelectedIndex = -1;

        }
        private static List<string> GetFuelList(string type)
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
    }
}
