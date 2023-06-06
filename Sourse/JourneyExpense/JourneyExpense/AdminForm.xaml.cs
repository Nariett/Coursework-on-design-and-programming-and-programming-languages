using JourneyExpense.EditWindows;
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
        private void AddCarButton_Click(object sender, RoutedEventArgs e) // обработчик нажатия на кнопку для октрытия формы для добавления автомобиля 
        {
            AddCarForm addCarForm = new AddCarForm();
            addCarForm.Show();
        }


        private void EditFuelButton_Click(object sender, RoutedEventArgs e)// обработчик нажатия на кнопку откртытия формы для редактирования топлива 
        {
            UpdatePriceForm updatePriceForm = new UpdatePriceForm();
            updatePriceForm.Show();
        }

        private void AddRoutesButton_Click(object sender, RoutedEventArgs e)// обработчик нажатия на кнопку для октрытия формы для добавления маршрута 
        {
            AddRoutesForm routesForm = new AddRoutesForm();
            routesForm.Show();
        }

        private void AddFuelButton_Click(object sender, RoutedEventArgs e)// обработчик нажатия на кнопку для октрытия формы для добавления топлива 
        {
            AddFuelForm fuelForm = new AddFuelForm();
            fuelForm.Show();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)// обработчик нажатия на кнопку для октрытия формы для открытия главного окна 
        {
            MainWindow adminMain = new MainWindow("Admin","Admin");
            adminMain.Show();
        }

        private void EditCarButton_Click(object sender, RoutedEventArgs e)// обработчик нажатия на кнопку откртытия формы для редактирования автомобиля
        {
            EditCarForm editCar = new EditCarForm();
            editCar.Show();
        }

        private void EditRoutesButton_Click(object sender, RoutedEventArgs e)// обработчик нажатия на кнопку откртытия формы для редактирования маршрута
        {
            EditRouteForm editRoute = new EditRouteForm();
            editRoute.Show();
        }
    }
}

