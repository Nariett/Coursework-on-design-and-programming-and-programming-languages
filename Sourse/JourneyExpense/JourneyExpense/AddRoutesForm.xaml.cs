using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JourneyExpense
{
    /// <summary>
    /// Логика взаимодействия для AddRoutesForm.xaml
    /// </summary>
    public partial class AddRoutesForm : Window
    {
        public AddRoutesForm()
        {
            InitializeComponent();
        }

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            string pointA = textBoxPointA.Text;
            string pointB = textBoxPointB.Text;
            double distance = 0;
            bool IsValidValue = IsValidDoubleInput(textBoxDistance, 0, 40000, out distance);
            if (IsValidValue && pointA != pointB)
            {
                Route route = new Route(pointA, pointB, distance);
                if (route.AddRoutesInXML())
                {
                    MessageBox.Show($"Маршрут {pointA} - {pointB} добавлен в систему.");
                }
                else
                {
                    MessageBox.Show($"Маршрут {pointA} - {pointB} не добавлен, так как уже есть в системе.");
                }
            }
            else
            {
                MessageBox.Show("Ошибка в маршруте, проверьте маршрут и повторите попытку.");
            }
        }
        private bool IsValidDoubleInput(TextBox box, int min, int max, out double value)
        {
            if (box.Text == "")
            {
                value = 0;
                box.BorderBrush = Brushes.Red;
                return false;
            }

            bool isNumeric = double.TryParse(FixStr(box.Text), out value);
            if (isNumeric)
            {
                if (value > min && value < max)
                {
                    box.BorderBrush = Brushes.Gray;
                    return true;
                }
                else
                {
                    box.BorderBrush = Brushes.Red;
                    return false;
                }
            }
            else
            {
                box.BorderBrush = Brushes.Red;
                return false;
            }
        }
        private string FixStr(string input)
        {
            return input.Replace('.', ',');
        }
    }
}
