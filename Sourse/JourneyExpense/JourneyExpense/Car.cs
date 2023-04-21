using System;
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
        public string FuelOctan { get; set; }//?
        public double FuelConsumptionGeneral { get; set; }
        public double FuelConsumptionCity { get; set; }//
        public double FuelConsumptionHighway { get; set; }//
        public double EnginePower { get; set; }
        public double EngineSize { get; set; }//
        public double TankSize { get; set; }
        public Car() { }
        public Car(string name, int year, string typeCar, int maxSpeed, int seatingCapacity, string fuel, string fuelOctan, double fuelConsumptionGeneral, double fuelConsumptionCity, double fuelConsumptionHighway, double enginePower, double engineSize, double tankSize)
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
            EngineSize = engineSize;
            TankSize = tankSize;
        }
        public void AddCarInXML()
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
                new XElement("fuelConsumptionGeneral", this.FuelConsumptionGeneral),
                new XElement("fuelConsumptionCity", this.FuelConsumptionCity),
                new XElement("fuelConsumptionHighway", this.FuelConsumptionHighway),
                new XElement("enginePower", this.EnginePower),
                new XElement("engineSize", this.EngineSize),
                new XElement("tankSize", this.TankSize)
            );
            root.Add(carElement);
            root.Save("Car.xml");
        }
    }
}

