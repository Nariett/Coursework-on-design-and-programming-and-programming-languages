using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyExpense
{
    class UserRoutes
    {
        public string user { get; set; }
        public string car { get; set; }
        public string pointA { get; set; }
        public string pointB { get; set; }
        public double distance { get; set; }
        public double price { get; set; }
        public DateTime date { get; set; }
        public UserRoutes() { }
        public UserRoutes(string user,string car, string pointA, string pointB, double distance, double price, DateTime date)
        {
            this.user = user;
            this.car = car;
            this.pointA = pointA;
            this.pointB = pointB;
            this.distance = distance;
            this.price = price;
            this.date = date;
        }
    }
}
