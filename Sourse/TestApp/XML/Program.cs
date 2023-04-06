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
            //Console.WriteLine("Выберите действие:\n1 - Создать XML файл\n2 - Читать данные из XML файла\n3 - Добавить данные в XML файл\n4 - удалить выбранный файл из XML файла");
            //int select = Convert.ToInt32(Console.ReadLine());
            while (true)
            {
                Console.WriteLine("Выберите действие:\n1 - Создать XML файл\n2 - Читать данные из XML файла\n3 - Добавить данные в XML файл\n4 - удалить выбранный файл из XML файла");
                int select = Convert.ToInt32(Console.ReadLine());
                switch (select)
                {
                    case 1:
                        {
                            Car myCar = new Car("Toyota Camry",
                                                2022,
                                                CarType.Sedan,
                                                130,
                                                5,
                                                FuelType.Gasoline,
                                                FuelOctan.AI92,
                                                8.5,
                                                10.2,
                                                5.8,
                                                203,
                                                2.5,
                                                60);
                            XDocument xdoc = new XDocument();
                            XElement car = new XElement("Car");
                            XElement carName = new XElement("name", myCar.Name);
                            XElement carYear = new XElement("year", myCar.Year);
                            XElement carType = new XElement("typeCar", myCar.TypeCar);
                            XElement carMaxSpeed = new XElement("maxSpeed", myCar.MaxSpeed);
                            XElement carSeatingCapacity = new XElement("seatingCapacity", myCar.SeatingCapacity);
                            XElement carFuel = new XElement("fuel", myCar.Fuel);
                            XElement carFuelOctan = new XElement("fuelOctan", myCar.FuelOctan);
                            XElement carFuelConsumptionGeneral = new XElement("fuelConsumptionGeneral", myCar.FuelConsumptionGeneral.ToString());//используется для добавления double в строку
                            XElement carFuelConsumptionCity = new XElement("fuelConsumptionCity", myCar.FuelConsumptionCity.ToString());
                            XElement carFuelConsumptionHighway= new XElement("fuelConsumptionHighway", myCar.FuelConsumptionHighway.ToString());
                            XElement carEnginePower = new XElement("enginePower", myCar.EnginePower.ToString());
                            XElement carEngineSize = new XElement("engineSize", myCar.EngineSize.ToString());
                            XElement carTankSize = new XElement("tankSize", myCar.TankSize.ToString());
                            car.Add(carName);
                            car.Add(carYear);
                            car.Add(carType);
                            car.Add(carMaxSpeed);
                            car.Add(carSeatingCapacity);
                            car.Add(carFuel);
                            car.Add(carFuelOctan);
                            car.Add(carFuelConsumptionGeneral);
                            car.Add(carFuelConsumptionCity);
                            car.Add(carFuelConsumptionHighway);
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
                                    if (childnode.Name == "fuelOctan")
                                    {
                                        car.FuelOctan = (FuelOctan)Enum.Parse(typeof(FuelOctan), childnode.InnerText);
                                        Console.WriteLine("Октановое числов топлива: {0}", car.FuelOctan);
                                    }
                                    if (childnode.Name == "fuelConsumptionGeneral")
                                    {
                                        car.FuelConsumptionGeneral = Convert.ToDouble(childnode.InnerText);
                                        Console.WriteLine("Смешанный расход топлива: {0}", car.FuelConsumptionGeneral);
                                    }
                                    if (childnode.Name == "fuelConsumptionCity")
                                    {
                                        car.FuelConsumptionCity = Convert.ToDouble(childnode.InnerText);
                                        Console.WriteLine("Расход топлива в городе: {0}", car.FuelConsumptionCity);
                                    }
                                    if (childnode.Name == "fuelConsumptionHighway")
                                    {
                                        car.FuelConsumptionHighway = Convert.ToDouble(childnode.InnerText);
                                        Console.WriteLine("Расход топлива за городом: {0}", car.FuelConsumptionHighway);
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
                            XmlElement carFuelOctan = xDoc.CreateElement("fuelOctan");
                            XmlElement carFuelConsumptionGeneral = xDoc.CreateElement("fuelConsumptionGeneral");
                            XmlElement carFuelConsumptionCity = xDoc.CreateElement("fuelConsumptionCity");
                            XmlElement carFuelConsumptionHighway = xDoc.CreateElement("fuelConsumptionHighway");
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
                            XmlText fuelText = xDoc.CreateTextNode(Console.ReadLine());
                            Console.Write("Введите октановое число топлива(AI87/AI92/AI95/DT/Electric):");
                            XmlText fuelOctanText = xDoc.CreateTextNode(Console.ReadLine());
                            Console.Write("Введите средний расход топлива на 100 км: ");
                            XmlText fuelConsumptionGeneralText = xDoc.CreateTextNode(FixStr(Console.ReadLine()));
                            Console.Write("Введите расход в городе топлива на 100 км: ");
                            XmlText fuelConsumptionCityText = xDoc.CreateTextNode(FixStr(Console.ReadLine()));
                            Console.Write("Введите расход за городом на 100 км: ");
                            XmlText fuelConsumptionHighwayText = xDoc.CreateTextNode(FixStr(Console.ReadLine()));
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
                            carFuelOctan.AppendChild(fuelOctanText);
                            carFuelConsumptionGeneral.AppendChild(fuelConsumptionGeneralText);
                            carFuelConsumptionCity.AppendChild(fuelConsumptionCityText);
                            carFuelConsumptionHighway.AppendChild(fuelConsumptionHighwayText);
                            carEnginePower.AppendChild(enginePowerText);
                            carEngineSize.AppendChild(engineSizeText);
                            carTankSize.AppendChild(tankSizeText);
                            car.AppendChild(carName);
                            car.AppendChild(carYear);
                            car.AppendChild(carTypeCar);
                            car.AppendChild(carMaxSpeed);
                            car.AppendChild(carSeatingCapacity);
                            car.AppendChild(carFuel);
                            car.AppendChild(carFuelConsumptionGeneral);
                            car.AppendChild(carFuelConsumptionCity);
                            car.AppendChild(carFuelConsumptionHighway);
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