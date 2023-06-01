using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Transactions;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Linq;

namespace JourneyExpense.Classes
{
    class UsersRoutes
    {
        public string User { get; set; }
        public string Car { get; set; }
        public string PointA { get; set; }
        public string PointB { get; set; }
        public double Distance { get; set; }
        public double Price { get; set; }
        public string Date { get; set; }
        public string FuelType { get; set; }
        public double UsedFuel { get; set; }
        public double AverageSpeed { get; set; }
        public UsersRoutes() { }
        public UsersRoutes(string user, string car, string pointA, string pointB, double distance, double price, string fuelType, string date, double usedfuel, double averageSpeed)
        {
            User = user;
            Car = car;
            PointA = pointA;
            PointB = pointB;
            Distance = distance;
            Price = price;
            FuelType = fuelType;
            Date = date;
            UsedFuel = usedfuel;
            AverageSpeed = averageSpeed;
        }
        public bool AddRoutesInXML()
        {

            XElement root = XElement.Load("UsersRoutes.xml");
            XElement carElement = new XElement("route",
                new XElement("User", User),
                new XElement("Car", Car),
                new XElement("PointA", PointA),
                new XElement("PointB", PointB),
                new XElement("Distance", FixStr(Distance)),
                new XElement("Price", FixStr(Price)),
                new XElement("FuelType", FuelType),
                new XElement("Date", Date),
                new XElement("UsedFuel", FixStr(UsedFuel)),
                new XElement("AverageSpeed", FixStr(AverageSpeed))
            );
            root.Add(carElement);
            root.Save("UsersRoutes.xml");
            return true;

        }
        public static List<UsersRoutes> ReadUsersRoutesInXML()
        {
            List<UsersRoutes> Route = new List<UsersRoutes>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("UsersRoutes.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                UsersRoutes route = new UsersRoutes();
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "User")
                    {
                        route.User = childnode.InnerText;
                    }
                    if (childnode.Name == "Car")
                    {
                        route.Car = childnode.InnerText;
                    }
                    if (childnode.Name == "PointA")
                    {
                        route.PointA = childnode.InnerText;
                    }
                    if (childnode.Name == "PointB")
                    {
                        route.PointB = childnode.InnerText;
                    }
                    if (childnode.Name == "Distance")
                    {
                        route.Distance = Convert.ToDouble(childnode.InnerText);
                    }
                    if (childnode.Name == "Price")
                    {
                        route.Price = Convert.ToDouble(childnode.InnerText);
                    }
                    if (childnode.Name == "FuelType")
                    {
                        route.FuelType = childnode.InnerText;
                    }
                    if (childnode.Name == "Date")
                    {
                        route.Date = childnode.InnerText;
                    }
                    if (childnode.Name == "UsedFuel")
                    {
                        route.UsedFuel = Convert.ToDouble(childnode.InnerText);
                    }
                    if (childnode.Name == "AverageSpeed")
                    {
                        route.AverageSpeed = Convert.ToDouble(childnode.InnerText);
                    }
                }
                Route.Add(route);
            }
            return Route;
        }
        public string FixStr(double x)
        {
            return x.ToString().Replace('.', ',');
        }
    }
}
