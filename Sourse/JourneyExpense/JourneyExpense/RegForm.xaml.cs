using System.Windows;
using System.Xml;

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
            if (textBoxLogin.Text != " " && textBoxPassword.Text != " " && textBoxName.Text != " " && textBoxPassword.Text != " ")
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
                MessageBox.Show("Элемент добавлен");
            }
            else { MessageBox.Show("Error"); }
        }/*private void Button_Click(object sender, RoutedEventArgs e)//созданеие xml file
        {
            User user = new User("Cre7po","1234567890","Иван","Тараскевич");
            XDocument xdoc = new XDocument();
            XElement User = new XElement("user");
            XElement userLogin = new XElement("login", user.login);
            XElement userPassword = new XElement("password", user.password);
            XElement userName = new XElement("name", user.name);
            XElement userSurname = new XElement("surname", user.surname);
            User.Add(userLogin);
            User.Add(userPassword);
            User.Add(userName);
            User.Add(userSurname);
            XElement users = new XElement("users");
            users.Add(User);
            xdoc.Add(users);
            xdoc.Save("User.xml");
        }*/
    }
}
