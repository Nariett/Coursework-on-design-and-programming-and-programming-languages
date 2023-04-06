using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML
{
    public enum CarType
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
    public enum FuelOctan///select 
    {
        AI87,
        AI92,
        AI95,
        AI98,///
        DT,
        Electric
    }
    [Serializable]
    public class Car
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public CarType TypeCar { get; set; }
        public int MaxSpeed { get; set; }
        public int SeatingCapacity { get; set; }
        public FuelType Fuel { get; set; }
        public FuelOctan FuelOctan { get; set; }
        public double FuelConsumptionGeneral { get; set; }
        public double FuelConsumptionCity { get; set; }
        public double FuelConsumptionHighway { get; set; }
        public double EnginePower { get; set; }
        public double EngineSize { get; set; }
        public double TankSize { get; set; }
        public Car() { }
        public Car(string name, int year, CarType typeCar, int maxSpeed, int seatingCapacity, FuelType fuel, FuelOctan fuelOctan,double fuelConsumptionGeneral, double fuelConsumptionCity, double fuelConsumptionHighway, double enginePower, double engineSize, double tankSize)
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
