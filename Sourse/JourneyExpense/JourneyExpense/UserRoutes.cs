using System;
using System.Transactions;

namespace JourneyExpense
{
    class UserRoutes
    {
        public string User { get; set; }
        public string Car { get; set; }
        public string PointA { get; set; }
        public string PointB { get; set; }
        public double Distance { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public double UsedFuel { get; set; }
        public UserRoutes() { }
        public UserRoutes(string user,string car, string pointA, string pointB, double distance, double price, DateTime date, double usedfuel)
        {
            this.User = user;
            this.Car = car;
            this.PointA = pointA;
            this.PointB = pointB;
            this.Distance = distance;
            this.Price = price;
            this.Date = date;
            this.UsedFuel = usedfuel;
        }
    }
}
