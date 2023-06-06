using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace JourneyExpense.Classes
{
    public class User
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
                // Читаем список пользователей из XML файла
                List<User> list = ReadUserInXML();

                // Проверяем, если уже существует пользователь с таким же логином
                foreach (var item in list)
                {
                    if (login == item.login)
                    {
                        MessageBox.Show("Данный логин уже занят. Повторите попытку.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }

                // Загружаем корневой элемент из XML файла
                XElement root = XElement.Load("User.xml");

                // Создаем новый элемент "user" с данными о новом пользователе
                XElement userElement = new XElement("user",
                    new XElement("login", login),
                    new XElement("password", password),
                    new XElement("name", name),
                    new XElement("surname", surname)
                );

                // Добавляем новый элемент "user" в корневой элемент
                root.Add(userElement);

                // Сохраняем изменения в XML файле
                root.Save("User.xml");

                return true;
            }
            catch (FileNotFoundException ex)
            {
                // Если файл User.xml не найден, создаем новый XML файл и сохраняем в него данные о пользователе
                XDocument xdoc = new XDocument();
                XElement user = new XElement("user",
                    new XElement("login", login),
                    new XElement("password", password),
                    new XElement("name", name),
                    new XElement("surname", surname)
                );
                XElement users = new XElement("users", user);
                xdoc.Add(users);
                xdoc.Save("User.xml");
                return true;
            }
            catch (Exception ex)
            {
                // В случае возникновения других исключений, возвращаем false
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
