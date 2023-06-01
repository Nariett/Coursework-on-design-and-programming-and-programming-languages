using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace JourneyExpense.Classes
{
    class User
    {
        public string login { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public User() { }
        public User(string login, string password, string name, string surname)
        {
            this.login = login;
            this.password = password;
            this.name = name;
            this.surname = surname;
        }
        public bool AddUserInXML()
        {
            try
            {
                List<User> list = ReadUserInXML();
                foreach (var item in list)
                {
                    if (login == item.login)
                    {
                        MessageBox.Show("Данный логин уже занят. Повторите попытку.", "Ошикбка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                XElement root = XElement.Load("User.xml");
                XElement carElement = new XElement("user",
                    new XElement("login", login),
                    new XElement("password", password),
                    new XElement("name", name),
                    new XElement("surname", surname)
                );
                root.Add(carElement);
                root.Save("User.xml");
                return true;
            }
            catch (FileNotFoundException ex)
            {
                XDocument xdoc = new XDocument();
                XElement User = new XElement("user",
                    new XElement("login", login),
                    new XElement("password", password),
                    new XElement("name", name),
                    new XElement("surname", surname)
                );
                XElement users = new XElement("users", User);
                xdoc.Add(users);
                xdoc.Save("User.xml");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static List<User> ReadUserInXML()
        {
            try
            {

                List<User> User = new List<User>();
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("User.xml");
                XmlElement xRoot = xDoc.DocumentElement;
                foreach (XmlNode xnode in xRoot)
                {
                    User user = new User();
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "login")
                        {
                            user.login = childnode.InnerText;
                        }
                        if (childnode.Name == "password")
                        {
                            user.password = childnode.InnerText;
                        }

                        if (childnode.Name == "name")
                        {
                            user.name = childnode.InnerText;
                        }
                        if (childnode.Name == "surname")
                        {
                            user.surname = childnode.InnerText;
                        }
                    }
                    User.Add(user);
                }
                return User;
            }
            catch (Exception ex)
            {
                return new List<User>();
            }
        }
    }
}
