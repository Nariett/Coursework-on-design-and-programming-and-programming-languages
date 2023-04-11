using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyExpense
{
   /* public enum CarType
    {
        Sedan,
        SUV,
        Coupe,
        Hatchback,
        Convertible,
        PickupTruck,
        Van
    }
    public enum FuelType
    {
        Gasoline,
        Diesel,
        Electric
    }
    public enum FuelOctan
    {
        AI92,
        AI95,
        AI98,
        DT,
        Electric,
    }*/
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
        public double EngineSize { get; set; }
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
    }
}

