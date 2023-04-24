using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Xml;
using System.Xml.Linq;

namespace JourneyExpense
{
    [Serializable]
    public class Car
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public string TypeCar { get; set; }
        public int MaxSpeed { get; set; }
        public int SeatingCapacity { get; set; }
        public string Fuel { get; set; }
        public string FuelOctan { get; set; }
        public double FuelConsumptionGeneral { get; set; }
        public double FuelConsumptionCity { get; set; }
        public double FuelConsumptionHighway { get; set; }
        public double EnginePower { get; set; }
        public double TankSize { get; set; }
        public Car() { }
        public Car(string name, int year, string typeCar, int maxSpeed, int seatingCapacity, string fuel, string fuelOctan, double fuelConsumptionGeneral, double fuelConsumptionCity, double fuelConsumptionHighway, double enginePower, double tankSize)
        {
            Name = name;
            Year = year;
            TypeCar = typeCar;
            MaxSpeed = maxSpeed;
            SeatingCapacity = seatingCapacity;
            Fuel = fuel;
            FuelOctan = fuelOctan;
            FuelConsumptionGeneral = fuelConsumptionGeneral;
            FuelConsumptionCity = fuelConsumptionCity;
            FuelConsumptionHighway = fuelConsumptionHighway;
            EnginePower = enginePower;
            TankSize = tankSize;
        }
        private List<Car> ReadCarInXML()
        {
            List<Car> Cars = new List<Car>();
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
                Cars.Add(car);
            }
            return Cars;
        }
        public bool AddCarInXML()
        {
            bool permission = true;
            foreach (var item in ReadCarInXML())
            {
                if (item.Name.ToLower() == this.Name.ToLower() && item.Year == this.Year)
                {
                    permission = false;
                }
            }
            if (permission)
            {
                XElement root = XElement.Load("Car.xml");
                XElement carElement = new XElement("car",
                    new XElement("name", this.Name),
                    new XElement("year", this.Year),
                    new XElement("typeCar", this.TypeCar),
                    new XElement("maxSpeed", this.MaxSpeed),
                    new XElement("seatingCapacity", this.SeatingCapacity),
                    new XElement("fuel", this.Fuel),
                    new XElement("fuelOctan", this.FuelOctan),
                    new XElement("fuelConsumptionGeneral", fixDouble(this.FuelConsumptionGeneral)),
                    new XElement("fuelConsumptionCity", fixDouble(this.FuelConsumptionCity)),
                    new XElement("fuelConsumptionHighway", fixDouble(this.FuelConsumptionHighway)),
                    new XElement("enginePower", this.EnginePower),
                    new XElement("tankSize", this.TankSize)
                );
                root.Add(carElement);
                root.Save("Car.xml");
                return true;
            }
            else
            {
                return false;
            }
        }
        public string fixDouble(double x)
        {
            return x.ToString().Replace('.', ',');
        }
    }

}

