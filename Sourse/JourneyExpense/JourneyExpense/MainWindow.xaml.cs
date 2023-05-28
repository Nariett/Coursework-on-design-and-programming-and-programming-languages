using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace JourneyExpense
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string userName)
        {
            InitializeComponent();
            InitList();
            InitComboBox();
            UserName = userName;
        }
        private string UserName;
        private bool isMessageBoxShown = false;
        private bool isMessageBoxShowPoint = false;
        private List<string> FuelList = new List<string>() { "Бензин", "Дизельное топливо", "Электричество" };
        private List<string> TypeConsuption = new List<string> { "Городской", "По трассе", "Смешанный" };
        private List<Car> CarList = new List<Car>();
        private List<Fuel> PriceList = new List<Fuel>();
        private List<Route> AllRoutes = new List<Route>();
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
            AllRoutes = Route.ReadRousteInXML();
            foreach (var item in AllRoutes)
            {
                if (!PointA.Contains(item.PointA))
                {
                    PointA.Add(item.PointA);
                }
                if (!PointA.Contains(item.PointB))
                {
                    PointA.Add(item.PointB);
                }
                if (!PointB.Contains(item.PointA))
                {
                    PointB.Add(item.PointA);
                }
                if (!PointB.Contains(item.PointB))
                {
                    PointB.Add(item.PointB);
                }
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
        private string SetValue(ComboBox comboBox)
        {
            if(comboBox.SelectedIndex != -1)
            {
                return comboBox.SelectedItem.ToString();
            }
            else { return "Неизвестно"; }
        }
        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            if(ValidValue())
            {
                string car = SetValue(comboBoxCar);
                string PointA = SetValue(comboBoxPointOne);
                string PointB = SetValue(comboBoxPointTwo);
                double dictance = Convert.ToDouble(this.textBoxDistance.Text);
                double fuelPrice = Convert.ToDouble(this.textBoxFuelPrice.Text);
                double consimption = Convert.ToDouble(this.textBoxConsumption.Text);
                double averageSpeed = Convert.ToDouble(this.textBoxAverSpeed.Text);
                double usedFuel = Math.Round(dictance/consimption);
                double result = dictance / averageSpeed;
                double fullPrice = Math.Round((dictance / 100) * fuelPrice * consimption, 2);
                this.textBoxUsedFuel.Text = usedFuel.ToString();
                this.textBoxTime.Text = Math.Round(result, 2).ToString();
                this.textBoxPrice.Text = fullPrice.ToString();
                UsersRoutes route = new UsersRoutes(UserName, car, PointA, PointB, dictance, fullPrice, usedFuel,averageSpeed);
                if(route.AddRoutesInXML())
                {
                    MessageBox.Show("Поездка оформлена");
                }
                else
                {
                    MessageBox.Show("Поездка не оформлена");
                }

            }
            else
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
                textBoxFuelPrice.IsReadOnly = true;
            }
            else
            {
                textBoxFuelPrice.IsReadOnly = false;
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
            textBoxUsedFuel.Clear();
            comboBoxFuelType.SelectedIndex = -1;
            comboBoxFuelOctan.SelectedIndex = -1;
            comboBoxCar.SelectedIndex = -1;
            comboBoxConsumption.SelectedIndex = -1;
            comboBoxPointOne.SelectedIndex = -1;
            comboBoxPointTwo.SelectedIndex = -1;

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

        private void comboBoxPointTwo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxPointOne.SelectedIndex != -1 && comboBoxPointTwo.SelectedIndex != -1 && comboBoxPointOne.SelectedItem != comboBoxPointTwo.SelectedItem)
            {
                string A = comboBoxPointOne.SelectedItem.ToString();
                string B = comboBoxPointTwo.SelectedItem.ToString();
                if (A != B)
                {
                    foreach (var item in AllRoutes)
                    {
                        if (item.PointA == A && item.PointB == B || item.PointA == B && item.PointB == A)
                        {
                            this.textBoxDistance.Text = item.Distance.ToString();
                            isMessageBoxShowPoint = false;
                            break;
                        }
                        else if (!isMessageBoxShowPoint)
                        {
                            MessageBox.Show("Маршрут не найден. Выберите существующий маршрут.");
                            isMessageBoxShowPoint = true;
                        }
                    }

                }
            }
            else if (comboBoxPointOne.SelectedIndex != -1 || comboBoxPointTwo.SelectedIndex != -1)
            {
                MessageBox.Show("Выберите корректное место назначения");
            }
        }
        private bool ValidValue()
        {
            double distance , averageSpeed, consumption,fuelPrice;
            if (IsValidDoubleInput(textBoxDistance, 0, 10000, out distance) &
                IsValidDoubleInput(textBoxAverSpeed, 0, 400, out averageSpeed) &
                IsValidDoubleInput(textBoxConsumption, 0, 100, out consumption) &
                IsValidDoubleInput(textBoxFuelPrice, 0, 1000, out fuelPrice))
            {
                return true;
            }
            else
            {
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
        private string FixStr(string input)
        {
            return input.Replace('.', ',');
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            GraphForm z = new GraphForm(UserName);
            z.Show();
        }
    }
}

