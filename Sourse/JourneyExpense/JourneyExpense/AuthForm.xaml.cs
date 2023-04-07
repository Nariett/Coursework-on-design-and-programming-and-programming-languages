using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace JourneyExpense
{
    /// <summary>
    /// Логика взаимодействия для AuthForm.xaml
    /// </summary>
    public partial class AuthForm : Window
    {
        public AuthForm()
        {
            InitializeComponent();
            //ReadUser();
        }
        private List<User> AllUsers = new List<User>();
        private void AuthButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы прошли аутентификаицю");
            MainWindow MainForm= new MainWindow();
            MainForm.Show();
        }

        private void RegButtonClick(object sender, RoutedEventArgs e)
        {
            RegForm RegForm = new RegForm();
            RegForm.Show();
        }
        public void ReadUser()
        {
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
                    if (childnode.Name == "maxSpeed")
                    {
                        user.surname = childnode.InnerText;
                    }
                }
                AllUsers.Add(user);
                Console.WriteLine("Все элементы отображены");
            }
        }
    }
}
