using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Linq;

namespace JourneyExpense.Classes
{
    public class Fuel
    {
        public string name { get; set; }
        public string octaneNumber { get; set; }
        public double price { get; set; }
        public Fuel() { }
        public Fuel(string name, string octaneNumber, double price)
        {
            this.name = name;
            this.octaneNumber = octaneNumber;
            this.price = price;
        }
        public static List<Fuel> ReadFuelInXML()//читать топливо из XML
        {
            List<Fuel> Fuel = new List<Fuel>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("Fuel.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
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
                Fuel.Add(fuel);
            }
            return Fuel;
        }
        public bool AddFuelInXML()//добавить топливо в XML
        {
            bool permission = true;
            foreach (var item in ReadFuelInXML())
            {
                if (item.octaneNumber == octaneNumber)
                {
                    permission = false;
                }
            }
            if (permission)
            {
                XElement root = XElement.Load("Fuel.xml");
                XElement routesElement = new XElement("fuel",
                    new XElement("name", name.TrimEnd()),
                    new XElement("octaneNumber", octaneNumber),
                    new XElement("price", FixStr(price))
                );
                root.Add(routesElement);
                root.Save("Fuel.xml");
                return true;
            }
            else
            {
                return false;
            }
        }
        public string FixStr(double x)
        {
            return x.ToString().Replace('.', ',');
        }
    }
}
