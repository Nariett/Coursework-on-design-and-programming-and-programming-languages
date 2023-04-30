﻿using System.Windows;

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
            UpdatePriceForm updatePriceForm = new UpdatePriceForm();
            updatePriceForm.Show();
        }

        private void AddRoutesButton_Click(object sender, RoutedEventArgs e)
        {
            AddRoutesForm routesForm = new AddRoutesForm();
            routesForm.Show();
        }
    }
}

