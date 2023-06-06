using System.Collections.Generic;
using System.Windows;
using JourneyExpense.Classes;

namespace JourneyExpense
{
    /// <summary>
    /// Логика взаимодействия для AuthForm.xaml
    /// </summary>
    public partial class AuthForm : Window
    {
        private List<User> AllUsers = new List<User>();
        private string surname = "";
        private bool Access = false;
        private string AdminLog = "Admin";
        private string AdminPass = "Admin";
        public AuthForm()
        {
            InitializeComponent();
        }
        private void AuthButtonClick(object sender, RoutedEventArgs e)
        {
            AllUsers = User.ReadUserInXML(); // инициализация списка AllUser данными из XML файла, путем вызова метода ReadUserInXML
            if(AllUsers.Count == 0) // Проверка на наличие пользователей в системе
            {
                MessageBox.Show("Зарегистрируйтесь в систему.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); // вывод сообщения
                return;
            }
            string login = textBoxLogin.Text; // декларация и инициализация переменной
            string password = textBoxPassword.Text;// декларация и инициализация переменной
            string userName = "";// декларация и инициализация переменной
            if (AdminAccess()) // проверка на вход администратора
            {
                AdminForm adminForm = new AdminForm(); // создание  экземпляра класса 
                adminForm.ShowDialog(); // отображение окна 
                return;
            }
            foreach (var user in AllUsers) 
            {
                if (user.login == login && user.password == password) // проверка логина и пароля в списке 
                {
                    userName = user.name;
                    surname = user.surname;
                    Access = true; 
                    break;
                }
            }
            if (Access)
            {
                MainWindow mainForm = new MainWindow(userName,surname); // создайние экземпляра класса 
                mainForm.ShowDialog();// открытие окна mainForm
            }
            else
            {
                MessageBox.Show("Ошибка ввода данных. Проверьте логин или пароль.", "Ошибка", MessageBoxButton.OK,MessageBoxImage.Error);// вывод сообщения 
            }
        }
        public bool AdminAccess() // проверка на вход администратора
        {
            if (textBoxLogin.Text == AdminLog && textBoxPassword.Text == AdminPass)
            {
                return true;
            }
            else return false;
        }
        private void RegButtonClick(object sender, RoutedEventArgs e)//обработчик нажатия на кнопку для открытия формы регисрации
        {
            RegForm RegForm = new RegForm();
            RegForm.ShowDialog();
        }
    }
}
