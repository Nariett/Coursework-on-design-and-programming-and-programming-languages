using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using JourneyExpense.Classes;

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
            if (TextBoxValid(textBoxLogin) & TextBoxValid(textBoxPassword) & TextBoxValid(textBoxName) & TextBoxValid(textBoxSurname))
            {
                User user = new User(textBoxLogin.Text, textBoxPassword.Text, textBoxName.Text, textBoxSurname.Text);
                if (user.AddUserInXML())
                {
                    MessageBox.Show("Пользователь добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Пользователь не добавлен. Возможно ошибка в том что:\n· Пустое поле\n· Длина поля меньше 3 символов\n· Использвание запрещенных символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool TextBoxValid(TextBox text)
        {
            if (text.Text != "" & Regex.IsMatch(text.Text, @"^[a-zA-Zа-яА-Я\s\d\-_]+$") & text.Text.Length > 3)
            {
                text.BorderBrush = Brushes.Gray;
                return true;
            }
            else
            {
                text.BorderBrush = Brushes.Red;
                return false;
            }
        }
    }
}
