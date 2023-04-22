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
    /// Логика взаимодействия для FuelForm.xaml
    /// </summary>
    public partial class FuelForm : Window
    {
        private List<Fuel> priceFuel = new List<Fuel>();
        public FuelForm()
        {
            InitializeComponent();
            InitList();
        }
        public void InitList()
        {
            priceFuel.Clear();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("Fuel.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                Fuel fuel = new Fuel();
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "octaneNumber")
                    {
                        fuel.octaneNumber = childnode.InnerText;
                    }

                    if (childnode.Name == "price")
                    {
                        fuel.price = Convert.ToDouble(childnode.InnerText);
                    }
                }
                priceFuel.Add(fuel);
            }
        }
    }
}
