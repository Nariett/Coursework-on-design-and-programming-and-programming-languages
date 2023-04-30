using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Xml;

namespace JourneyExpense
{
    class Fuel
    {
        public string name { get; set; }
        public string octaneNumber { get; set; }
        public double price { get; set; }
        public Fuel() { }
        public Fuel(string name,string octaneNumber, double price)
        {
            this.name = name;
            this.octaneNumber = octaneNumber;
            this.price = price;
        }
        public List<Fuel> ReadFuelInXML()
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
                        fuel.octaneNumber =childnode.InnerText;
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

    }
}
