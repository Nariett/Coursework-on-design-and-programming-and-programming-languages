using System;
using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace JourneyExpense
{
    /// <summary>
    /// Логика взаимодействия для RegForm.xaml
    /// </summary>
    public partial class RegForm : Window
    {
        public RegForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxLogin.Text != " " && textBoxPassword.Text != " " && textBoxName.Text != " " && textBoxPassword.Text != " " )
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load("User.xml"); 
                    XmlElement xRoott = xDoc.DocumentElement;
                    XmlElement user = xDoc.CreateElement("user");
                    XmlElement userLogin = xDoc.CreateElement("login");
                    XmlElement userPassword = xDoc.CreateElement("password");
                    XmlElement userName = xDoc.CreateElement("name");
                    XmlElement userSurname = xDoc.CreateElement("surname");
                    XmlText userLoginText = xDoc.CreateTextNode(textBoxLogin.Text);
                    XmlText userPasswordText = xDoc.CreateTextNode(textBoxPassword.Text);
                    XmlText userNameText = xDoc.CreateTextNode(textBoxName.Text);
                    XmlText userSurnameText = xDoc.CreateTextNode(textBoxSurname.Text);
                    userLogin.AppendChild(userLoginText);
                    userPassword.AppendChild(userPasswordText);
                    userName.AppendChild(userNameText);
                    userSurname.AppendChild(userSurnameText);
                    user.AppendChild(userLogin);
                    user.AppendChild(userPassword);
                    user.AppendChild(userName);
                    user.AppendChild(userSurname);
                    xRoott.AppendChild(user);
                    xDoc.Save("User.xml");
                    MessageBox.Show("Пользователь добавлен");
                }
                else { MessageBox.Show("Введены некорректнеые данные."); }
            }
            catch(FileNotFoundException ex)
            {
                XDocument xdoc = new XDocument();
                XElement User = new XElement("user");
                XElement userLogin = new XElement("login", textBoxLogin.Text);
                XElement userPassword = new XElement("password",textBoxPassword.Text);
                XElement userName = new XElement("name", textBoxName.Text);
                XElement userSurname = new XElement("surname", textBoxSurname.Text);
                User.Add(userLogin);
                User.Add(userPassword);
                User.Add(userName);
                User.Add(userSurname);
                XElement users = new XElement("users");
                users.Add(User);
                xdoc.Add(users);
                xdoc.Save("User.xml");
                MessageBox.Show("Пользователь добавлен");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка {ex.Message}");
            }
        }
        //public bool validUser()
    }
}
