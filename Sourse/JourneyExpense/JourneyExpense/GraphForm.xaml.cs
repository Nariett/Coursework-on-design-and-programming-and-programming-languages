using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace JourneyExpense
{
    /// <summary>
    /// Логика взаимодействия для GraphForm.xaml
    /// </summary>
    public partial class GraphForm : Window
    {
        private string UserName;
        private List<UsersRoutes> FilterListRoutes = new List<UsersRoutes>();
        private List<UsersRoutes> AllRoutes = UsersRoutes.ReadUsersRoutesInXML().OrderBy(r => DateTime.ParseExact(r.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture)).ToList();
        public GraphForm(string userName)
        {
            UserName = userName;
            InitializeComponent();
            ReadData();
            CreateGraph();
            DrawGraph();
        }
        private void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            if (DataPickerFirstData.SelectedDate.HasValue && DataPickerSecondData.SelectedDate.HasValue)
            {
                DateTime dateOne = DataPickerFirstData.SelectedDate.Value;
                DateTime dateTwo = DataPickerSecondData.SelectedDate.Value;

                if (dateOne < dateTwo)
                {
                    canvasGraph.Children.Clear();
                    CreateGraph();
                    string dateOneStr = dateOne.ToString("dd.MM.yyyy");
                    string dateTwoStr = dateTwo.ToString("dd.MM.yyyy");
                    DrawGraphInDate(dateOneStr, dateTwoStr);
                }
                else
                {
                    MessageBox.Show("Введены некорректные даты");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        public void ReadData()
        {
            foreach (var route in AllRoutes)
            {
                if (route.User == UserName)
                {
                    FilterListRoutes.Add(route);
                    //DatePrice.Add(new Tuple<string, double>(route.Date, route.Price));
                }
            }
        }
        private void CreateGraph()
        {
            DrawHorizontalLine();
            DrawVerticalLine();
        }
        private void DrawVerticalLine()
        {
            int left = 0;
            int step = 10;

            while (left <= 600)
            {
                Line line = new Line
                {
                    X1 = 0,
                    Y1 = 0,
                    X2 = 0,
                    Y2 = 380,
                    Stroke = Brushes.Gray,

                    StrokeThickness = 0.5
                };
                canvasGraph.Children.Add(line);
                Canvas.SetLeft(line, left);
                left += step;
            }
        }
        private void DrawHorizontalLine()
        {
            int top = 380;
            int step = 10;

            while (top >= 0)
            {
                Line line = new Line
                {
                    X1 = 0,
                    Y1 = 0,
                    X2 = 600,
                    Y2 = 0,
                    Stroke = Brushes.Gray,
                    StrokeThickness = 0.5
                };
                canvasGraph.Children.Add(line);
                Canvas.SetTop(line, top);
                top -= step;
            }
        }
        private void DrawGraph()
        {
            if (FilterListRoutes.Count != 0)
            {
                Polyline polyline = new Polyline();
                polyline.Stroke = Brushes.Blue;
                polyline.StrokeThickness = 2;
                int count = FilterListRoutes.Count;
                double price = FilterListRoutes.Max(item => item.Price);
                double stepX = 600 / count;//поездки
                double stepY = 380 / price;//цена поездки
                for (int i = 0; i < count; i++)
                {
                    double x = i * stepX;
                    double y = 380 - (FilterListRoutes[i].Price * stepY);
                    polyline.Points.Add(new Point(x, y));
                    AddPoint(stepX, stepY, i, FilterListRoutes);
                    AddTextBlock(FilterListRoutes, i, x, y);
                    AddAxis(i + 1, 0 + (stepX * i), 380);
                }
                double step = price / 30;
                for (double i = 0; i <= price; i += step)
                {
                    AddAxisY(i, -10, 370 - (i * stepY));
                }
                if (polyline.Parent is Panel panel)
                {
                    panel.Children.Remove(polyline);
                }
                canvasGraph.Children.Add(polyline);
            }
            else
            {
                MessageBox.Show("Создайте первую поездку и повторите попытку.");
            }
        }
        private void DrawGraphInDate(string dateOne, string dateTwo)
        {
            DateTime fromDate = DateTime.ParseExact(dateOne, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(dateTwo, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            List<UsersRoutes> filteredList = FilterListRoutes
                .Where(item => DateTime.ParseExact(item.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture) >= fromDate &&
                               DateTime.ParseExact(item.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture) <= toDate).ToList();
            if (filteredList.Count != 0)
            {
                Polyline polyline = new Polyline();
                polyline.Stroke = Brushes.Blue;
                polyline.StrokeThickness = 2;
                int count = filteredList.Count;
                double price = filteredList.Max(item => item.Price);
                double stepX = 600 / count;
                double stepY = 380 / price;
                double totalPrice = 0;
                for (int i = 0; i < count; i++)
                {
                    totalPrice += filteredList[i].Price;
                    double x = i * stepX;
                    double y = 380 - (filteredList[i].Price * stepY);
                    polyline.Points.Add(new Point(x, y));
                    AddPoint(stepX, stepY, i, filteredList);
                    AddTextBlock(filteredList, i, x, y);
                    AddAxis(i + 1, 0 + (stepX * i), 380);
                }
                double step = price / 30;
                for (double i = 0; i <= price; i += step)
                {
                    AddAxisY(i, -10, 370 - (i * stepY));
                }
                TotalPrice.Content = "Итог за выбранный срок: \n" + Math.Round(totalPrice,2).ToString() + " Рублей";
                if (polyline.Parent is Panel panel)
                {
                    panel.Children.Remove(polyline);
                }
                canvasGraph.Children.Add(polyline);
            }
            else
            {
                MessageBox.Show("Поезкди в данный период времени не обнаружены.");
            }
        }
        private void AddAxis(int i, double x, double y)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 10;
            textBlock.Text = i.ToString();
            textBlock.Margin = new Thickness(x, y, 0, 0);
            canvasGraph.Children.Add(textBlock);
        }
        private void AddAxisY(double i, double x, double y)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 10;
            textBlock.Text = Math.Round(i).ToString();
            textBlock.Margin = new Thickness(x, y, 0, 0);

            canvasGraph.Children.Add(textBlock);
        }
        private void AddPoint(double stepX, double stepY, int i, List<UsersRoutes> list)
        {
            Ellipse redPoint = new Ellipse();
            redPoint.Width = 6;
            redPoint.Height = 6;
            redPoint.Fill = Brushes.DarkBlue;
            redPoint.Margin = new Thickness(i * stepX - 2, 380 - (list[i].Price * stepY) - 2, 0, 0);
            canvasGraph.Children.Add(redPoint);
        }
        private void AddTextBlock(List<UsersRoutes> list, int i, double x, double y)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = list[i].Price.ToString();
            textBlock.Margin = new Thickness(x + 4, y - 16, 0, 0);
            canvasGraph.Children.Add(textBlock);
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            canvasGraph.Children.Clear();
        }

        private void ButtonShowAll_Click(object sender, RoutedEventArgs e)
        {
            canvasGraph.Children.Clear();
            CreateGraph();
            DrawGraph();
        }

        private void ButtonCreateView_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
