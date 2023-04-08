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
        private bool access = false;
        private void AuthButtonClick(object sender, RoutedEventArgs e)
        {
            ReadUser();
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if (AllUsers[i].login == textBoxLogin.Text && AllUsers[i].password == textBoxPassword.Text)
                {
                    access = true;
                    break;
                }
                else { access = false; }
            }
            if (access)
            {
                MainWindow mainForm = new MainWindow();
                mainForm.ShowDialog();
            }else { MessageBox.Show("Ошибка ввода данных. Проверьте логин или пароль."); }
        }

        private void RegButtonClick(object sender, RoutedEventArgs e)
        {
            RegForm RegForm = new RegForm();
            RegForm.Show();
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
                    Console.WriteLine("Все элементы отображены");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Создайте аккаут чтобы зайти");
            }

        }
    }
}
