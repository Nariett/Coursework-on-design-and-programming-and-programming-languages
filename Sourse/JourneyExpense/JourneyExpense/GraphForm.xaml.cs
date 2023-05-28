using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
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
        private int CountRoutes = 0;
        List<Tuple<string, double>> DatePrice = new List<Tuple<string, double>>();
        private List<UsersRoutes> AllRoutes = UsersRoutes.ReadUsersRoutesInXML();
        public GraphForm(string userName)
        {
            UserName = userName;
            InitializeComponent();
            ReadData();
            DrawLine();
            DrawGraph();
        }
        public void ReadData()
        {
            foreach (var route in AllRoutes)
            {
                if (route.User == UserName)
                {
                    CountRoutes++;
                    DatePrice.Add(new Tuple<string, double>(route.Date, route.Price));
                }
            }
        }
        private void DrawLine()
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
                    Stroke = Brushes.Black,
                    StrokeThickness = 0.5
                };
                canvasGraph.Children.Add(line);
                Canvas.SetTop(line, top);
                top -= step;
            }
            //добавить боковые границы
        }
        private void DrawGraph()
        {
            Polyline polyline = new Polyline();
            polyline.Stroke = Brushes.Blue;
            polyline.StrokeThickness = 2;
            int count = DatePrice.Count;
            double price = DatePrice.Max(item => item.Item2);
            double stepX = 600 / count;//поездки
            double stepY = 380 / price;//цена поездки
            for (int i = 0; i < count; i++)
            {
                double x = i * stepX;
                double y = 380 - (DatePrice[i].Item2 * stepY);
                polyline.Points.Add(new Point(x, y));
                AddPoint(stepX, stepY, i,DatePrice);
                AddTextBlock(i, x, y);
            }
            if (polyline.Parent is Panel panel)
            {
                panel.Children.Remove(polyline);
            }
            canvasGraph.Children.Add(polyline);
        }
        private void AddPoint(double stepX, double stepY, int i, List<Tuple<string, double>> list)
        {
            Ellipse redPoint = new Ellipse();
            redPoint.Width = 5;
            redPoint.Height = 5;
            redPoint.Fill = Brushes.Red;
            redPoint.Margin = new Thickness(i * stepX - 2, 380 - (list[i].Item2 * stepY) - 2, 0, 0);
            canvasGraph.Children.Add(redPoint);
        }
        private void DrawGraphInDate(string dateOne, string dateTwo)
        {
            DateTime fromDate = DateTime.ParseExact(dateOne, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(dateTwo, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            List<Tuple<string, double>> filteredList = DatePrice
                .Where(item => DateTime.ParseExact(item.Item1, "dd.MM.yyyy", CultureInfo.InvariantCulture) >= fromDate &&
                               DateTime.ParseExact(item.Item1, "dd.MM.yyyy", CultureInfo.InvariantCulture) <= toDate).ToList();
            Polyline polyline = new Polyline();
            polyline.Stroke = Brushes.Blue;
            polyline.StrokeThickness = 2;
            int count = filteredList.Count;
            double price = filteredList.Max(item => item.Item2);
            double stepX = 600 / count;//поездки
            double stepY = 380 / price;//цена поездки
            double totalPrice = 0;
            for (int i = 0; i < count; i++)
            {
                totalPrice += filteredList[i].Item2;
                double x = i * stepX;
                double y = 380 - (filteredList[i].Item2 * stepY);
                polyline.Points.Add(new Point(x, y));
                AddPoint(stepX, stepY, i, filteredList);
                AddTextBlock(i, x, y);
            }
            TotalPrice.Content = "Итог за выбранный срок: \n"+ totalPrice.ToString() + " Рублей";
            if (polyline.Parent is Panel panel)
            {
                panel.Children.Remove(polyline);
            }
            canvasGraph.Children.Add(polyline);
        }
        private void AddTextBlock(int i, double x, double y)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = DatePrice[i].Item2.ToString();
            textBlock.Margin = new Thickness(x + 4, y - 16, 0, 0);
            canvasGraph.Children.Add(textBlock);
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
                    DrawLine();
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
    }
}
