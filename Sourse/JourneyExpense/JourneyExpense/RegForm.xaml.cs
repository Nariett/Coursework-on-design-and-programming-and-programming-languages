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
            // Проверка валидности текстовых полей textBoxLogin, textBoxPassword, textBoxName, textBoxSurname
            if (TextBoxValid(textBoxLogin) & TextBoxValid(textBoxPassword) & TextBoxValid(textBoxName) & TextBoxValid(textBoxSurname))
            {
                // Создание нового объекта User с данными из текстовых полей
                User user = new User(textBoxLogin.Text, textBoxPassword.Text, textBoxName.Text, textBoxSurname.Text);

                // Добавление пользователя в XML файл и проверка успешности операции
                if (user.AddUserInXML())
                {
                    // Если пользователь успешно добавлен, выводим сообщение об успешном добавлении
                    MessageBox.Show("Пользователь добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close(); // Закрытие текущего окна
                }
            }
            else
            {
                // Если хотя бы одно из текстовых полей не прошло валидацию,
                // выводим сообщение об ошибке с указанием возможных причин
                MessageBox.Show("Пользователь не добавлен. Возможно ошибка в том что:\n· Пустое поле\n· Длина поля меньше 3 символов\n· Использование запрещенных символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool TextBoxValid(TextBox text)
        {
            if (text.Text != "" & Regex.IsMatch(text.Text, @"^[a-zA-Zа-яА-Я\s\d\-_]+$") & text.Text.Length > 3 & text.Text != "Admin")
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
