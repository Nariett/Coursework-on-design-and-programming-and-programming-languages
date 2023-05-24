using System;
using System.Transactions;
using System.Windows.Markup;
using System.Xml.Linq;

namespace JourneyExpense
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
        public double UsedFuel { get; set; }
        public double AverageSpeed { get; set; }
        public UsersRoutes() { }
        public UsersRoutes(string user, string car, string pointA, string pointB, double distance, double price, double usedfuel, double averageSpeed)
        {
            this.User = user;
            this.Car = car;
            this.PointA = pointA;
            this.PointB = pointB;
            this.Distance = distance;
            this.Price = price;
            DateTime data = DateTime.Now;
            this.Date = $"{data.Day:00}.{data.Month:00}.{data.Year}";
            this.UsedFuel = usedfuel;
            this.AverageSpeed = averageSpeed;
        }
        public bool AddRoutesInXML()
        {

            XElement root = XElement.Load("UsersRoutes.xml");
            XElement carElement = new XElement("route",
                new XElement("User", this.User),
                new XElement("Car", this.Car),
                new XElement("PointA", this.PointA),
                new XElement("PointB", this.PointB),
                new XElement("Distance", FixStr(this.Distance)),
                new XElement("Price", FixStr(this.Price)),
                new XElement("Date", this.Date),
                new XElement("UsedFuel", FixStr(this.UsedFuel)),
                new XElement("AverageSpeed", FixStr(this.AverageSpeed))
            );
            root.Add(carElement);
            root.Save("UsersRoutes.xml");
            return true;

        }
        public string FixStr(double x)
        {
            return x.ToString().Replace('.', ',');
        }
    }
}
