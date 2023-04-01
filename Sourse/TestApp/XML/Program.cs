using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Resolvers;

namespace XML
{
    class Program
    {
        public static List<Car> AllCars = new List<Car>();
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите действие:\n1 - Создать XML файл\n2 - Читать данные из XML файла\n3 - Добавить данные в XML файл\n4 - удалить выбранный файл из XML файла");
            int select = Convert.ToInt32(Console.ReadLine());
            while (true)
            {
                switch (select)
                {
                    case 1:
                        {
                            Car myCar = new Car
                            {
                                Name = "Toyota Camry",
                                Year = 2022,
                                TypeCar = CarType.Sedan,
                                MaxSpeed = 180,
                                SeatingCapacity = 5,
                                Fuel = FuelType.Gasoline,
                                FuelConsumption = 7.5,
                                EnginePower = 203,
                                EngineSize = 2.5,
                                TankSize = 60
                            };
                            XDocument xdoc = new XDocument();
                            XElement car = new XElement("Car");
                            XElement carName = new XElement("name", myCar.Name);
                            XElement carYear = new XElement("year", myCar.Year);
                            XElement carType = new XElement("typeCar", myCar.TypeCar);
                            XElement carMaxSpeed = new XElement("maxSpeed", myCar.MaxSpeed);
                            XElement carSeatingCapacity = new XElement("seatingCapacity", myCar.SeatingCapacity);
                            XElement carFuel = new XElement("fuel", myCar.Fuel);
                            XElement carFuelConsumption = new XElement("fuelConsumption", myCar.FuelConsumption.ToString());//используется для добавления double в строку
                            XElement carEnginePower = new XElement("enginePower", myCar.EnginePower.ToString());
                            XElement carEngineSize = new XElement("engineSize", myCar.EngineSize.ToString());
                            XElement carTankSize = new XElement("tankSize", myCar.TankSize.ToString());
                            car.Add(carName);
                            car.Add(carYear);
                            car.Add(carType);
                            car.Add(carMaxSpeed);
                            car.Add(carSeatingCapacity);
                            car.Add(carFuel);
                            car.Add(carFuelConsumption);
                            car.Add(carEnginePower);
                            car.Add(carEngineSize);
                            car.Add(carTankSize);
                            car.Add(carTankSize);
                            XElement phones = new XElement("Cars");
                            phones.Add(car);
                            xdoc.Add(phones);
                            xdoc.Save("Car.xml");
                            Console.WriteLine("Создан объект");
                            break;
                        }
                    case 2:
                        {
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
                                        Console.WriteLine("Название автомобиля: {0}", car.Name);
                                    }
                                    if (childnode.Name == "year")
                                    {
                                        car.Year = Convert.ToInt32(childnode.InnerText);
                                        Console.WriteLine("Год выпуска: {0}", car.Year);
                                    }

                                    if (childnode.Name == "typeCar")
                                    {
                                        car.TypeCar = (CarType)Enum.Parse(typeof(CarType), childnode.InnerText);
                                        Console.WriteLine("Тип автомобиля: {0}", car.TypeCar);
                                    }
                                    if (childnode.Name == "maxSpeed")
                                    {
                                        car.MaxSpeed = Convert.ToInt32(childnode.InnerText);
                                        Console.WriteLine("Максимальная скорость: {0}", car.MaxSpeed);
                                    }
                                    if (childnode.Name == "seatingCapacity")
                                    {
                                        car.SeatingCapacity = Convert.ToInt32(childnode.InnerText);
                                        Console.WriteLine("Количество мест: {0}", car.SeatingCapacity);
                                    }
                                    if (childnode.Name == "fuel")
                                    {
                                        car.Fuel = (FuelType)Enum.Parse(typeof(FuelType), childnode.InnerText);
                                        Console.WriteLine("Тип топлива: {0}", car.Fuel);
                                    }
                                    if (childnode.Name == "fuelConsumption")
                                    {
                                        car.FuelConsumption = Convert.ToDouble(car.FuelConsumption);
                                        Console.WriteLine("Расход топлива: {0}", car.FuelConsumption);
                                    }
                                    if (childnode.Name == "enginePower")
                                    {
                                        car.EnginePower = Convert.ToDouble(childnode.InnerText);
                                        Console.WriteLine("Мощность двигателя: {0}", car.EnginePower);
                                    }
                                    if (childnode.Name == "engineSize")
                                    {
                                        car.EngineSize = Convert.ToDouble(childnode.InnerText);
                                        Console.WriteLine("Объем двигателя: {0}", car.EngineSize);
                                    }
                                    if (childnode.Name == "tankSize")
                                    {
                                        car.TankSize = Convert.ToDouble(childnode.InnerText);
                                        Console.WriteLine("Объем топливного бака: {0}", car.TankSize);
                                    }
                                }
                                AllCars.Add(car);
                                Console.WriteLine("Все элементы отображены");
                            }
                            break;
                        }
                    case 3:
                        {
                            XmlDocument xDoc = new XmlDocument();
                            xDoc.Load("Car.xml");
                            XmlElement xRoott = xDoc.DocumentElement;
                            XmlElement car = xDoc.CreateElement("car");
                            XmlElement carName = xDoc.CreateElement("name");
                            XmlElement carYear = xDoc.CreateElement("year");
                            XmlElement carTypeCar = xDoc.CreateElement("typeCar");
                            XmlElement carMaxSpeed = xDoc.CreateElement("maxSpeed");
                            XmlElement carSeatingCapacity = xDoc.CreateElement("seatingCapacity");
                            XmlElement carFuel = xDoc.CreateElement("fuel");
                            XmlElement carFuelConsumption = xDoc.CreateElement("fuelConsumption");
                            XmlElement carEnginePower = xDoc.CreateElement("enginePower");
                            XmlElement carEngineSize = xDoc.CreateElement("engineSize");
                            XmlElement carTankSize = xDoc.CreateElement("tankSize");
                            Console.Write("Введите название автомобиля: ");
                            XmlText nameText = xDoc.CreateTextNode(Console.ReadLine());
                            Console.Write("Введите год выпуска автомобиля: ");
                            XmlText yearText = xDoc.CreateTextNode(Console.ReadLine());
                            Console.Write("Введите тип автомобиля (Sedan,SUV, Coupe,Hatchback,Convertible,PickupTruck,Van):");
                            XmlText typeCarText = xDoc.CreateTextNode(Console.ReadLine());
                            Console.Write("Введите максимальную скорость автомобиля: ");
                            XmlText maxSpeedText = xDoc.CreateTextNode(Console.ReadLine());
                            Console.Write("Введите количество мест в автомобиле: ");
                            XmlText seatingCapacityText = xDoc.CreateTextNode(Console.ReadLine());
                            Console.Write("Введите тип топлива (Gasoline/Diesel/Electric):");
                            XmlText fuelText = xDoc.CreateTextNode(Console.ReadLine()); ;
                            Console.Write("Введите расход топлива на 100 км: ");
                            XmlText fuelConsumptionText = xDoc.CreateTextNode(FixStr(Console.ReadLine()));
                            Console.Write("Введите мощность двигателя автомобиля: ");
                            XmlText enginePowerText = xDoc.CreateTextNode(FixStr(Console.ReadLine()));
                            Console.Write("Введите объем двигателя автомобиля: ");
                            XmlText engineSizeText = xDoc.CreateTextNode(FixStr(Console.ReadLine()));
                            Console.Write("Введите размер топливного бака: ");
                            XmlText tankSizeText = xDoc.CreateTextNode(FixStr(Console.ReadLine()));
                            carName.AppendChild(nameText);
                            carYear.AppendChild(yearText);
                            carTypeCar.AppendChild(typeCarText);
                            carMaxSpeed.AppendChild(maxSpeedText);
                            carSeatingCapacity.AppendChild(seatingCapacityText);
                            carFuel.AppendChild(fuelText);
                            carFuelConsumption.AppendChild(fuelConsumptionText);
                            carEnginePower.AppendChild(enginePowerText);
                            carEngineSize.AppendChild(engineSizeText);
                            carTankSize.AppendChild(tankSizeText);
                            car.AppendChild(carName);
                            car.AppendChild(carYear);
                            car.AppendChild(carTypeCar);
                            car.AppendChild(carMaxSpeed);
                            car.AppendChild(carSeatingCapacity);
                            car.AppendChild(carFuel);
                            car.AppendChild(carFuelConsumption);
                            car.AppendChild(carEnginePower);
                            car.AppendChild(carEngineSize);
                            car.AppendChild(carTankSize);
                            xRoott.AppendChild(car);
                            xDoc.Save("Car.xml");
                            Console.WriteLine("Элемент добавлен");
                            break;
                        }
                    case 4:
                        {
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.Load("Car.xml");
                            Console.WriteLine("Введите назввание автомобиля, который желаете удалить: ");
                            XmlNode carNode = xmlDoc.SelectSingleNode($"//car[name='{Console.ReadLine()}']");
                            XmlNode parent = carNode.ParentNode;
                            parent.RemoveChild(carNode);
                            xmlDoc.Save("cars.xml");
                            Console.WriteLine("Элемент удален");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Введенеы неверные данные");
                            break;
                        }
                }
            }
        }
        public static string FixStr(string input)
        {
            return input.Replace(".", ",");
        }
    }
}