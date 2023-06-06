﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace JourneyExpense.Classes
{
    public class Route
    {
        public string PointA { get; set; }
        public string PointB { get; set; }
        public double Distance { get; set; }
        public Route() { }
        public Route(string pointA, string pointB, double distance)
        {
            PointA = pointA;
            PointB = pointB;
            Distance = distance;
        }
        public static List<Route> ReadRousteInXML()//прочитать данные о поездках из XML
        {
            List<Route> Route = new List<Route>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("Routes.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                Route route = new Route();
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "PointA")
                    {
                        route.PointA = childnode.InnerText;
                    }
                    if (childnode.Name == "PointB")
                    {
                        route.PointB = childnode.InnerText;
                    }

                    if (childnode.Name == "kilometer")
                    {
                        route.Distance = Convert.ToDouble(childnode.InnerText);
                    }
                }
                Route.Add(route);
            }
            return Route;
        }
        public bool AddRoutesInXML()//добавить маршрут в XML
        {
            bool permission = true;
            foreach (var item in ReadRousteInXML())
            {
                if (item.PointA == PointA && item.PointB == PointB || item.PointA == PointB && item.PointB == PointA)
                {
                    permission = false;
                }
            }
            if (permission)
            {
                XElement root = XElement.Load("Routes.xml");
                XElement routesElement = new XElement("route",
                    new XElement("PointA", PointA),
                    new XElement("PointB", PointB),
                    new XElement("kilometer", FixStr(Distance))
                );
                root.Add(routesElement);
                root.Save("Routes.xml");
                return true;
            }
            else
            {
                return false;
            }
        }
        public string FixStr(double x)
        {
            return x.ToString().Replace('.', ',');
        }
    }
}

