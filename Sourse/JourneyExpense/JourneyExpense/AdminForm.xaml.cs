using System.Windows;

namespace JourneyExpense
{
    /// <summary>
    /// Логика взаимодействия для AdminForm.xaml
    /// </summary>
    public partial class AdminForm : Window
    {
        public AdminForm()
        {
            InitializeComponent();
        }
        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            AddCarForm addCarForm = new AddCarForm();
            addCarForm.Show();
        }

        private void EditFuelButton_Click(object sender, RoutedEventArgs e)
        {
            FuelForm fuelForm = new FuelForm();
            fuelForm.Show();
        }

        private void AddRoutesButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

