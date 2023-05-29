using System;
using System.Collections.Generic;
using System.Windows;
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
        }

        private List<User> AllUsers = new List<User>();
        private bool Access = false;
        private string AdminLog = "Admin";
        private string AdminPass = "Admin";
        private void AuthButtonClick(object sender, RoutedEventArgs e)
        {
            ReadUser();
            string login = textBoxLogin.Text;
            string password = textBoxPassword.Text;
            string userName = "";
            if (AdminAccess())
            {
                AdminForm adminForm= new AdminForm();
                adminForm.Show();
                return;
            }
            foreach (var user in AllUsers)
            {
                if (user.login == login && user.password == password)
                {
                    userName = user.name;
                    Access = true;
                    break;
                }
            }
            if (Access)
            {
                MainWindow mainForm = new MainWindow(userName);
                mainForm.ShowDialog();
            }
            else 
            {
                MessageBox.Show("Ошибка ввода данных. Проверьте логин или пароль.","Ошибка"); 
            }
        }

        private void RegButtonClick(object sender, RoutedEventArgs e)
        {
            RegForm RegForm = new RegForm();
            RegForm.Show();
        }

        public bool AdminAccess()
        {
            if (textBoxLogin.Text == AdminLog && textBoxPassword.Text == AdminPass)
            {
                return true;
            }
            else return false;
        }

        public void ReadUser()
        {
            try
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
                        if (childnode.Name == "surname")
                        {
                            user.surname = childnode.InnerText;
                        }
                    }
                    AllUsers.Add(user);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Создайте аккаут чтобы зайти", "Ошибка");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
