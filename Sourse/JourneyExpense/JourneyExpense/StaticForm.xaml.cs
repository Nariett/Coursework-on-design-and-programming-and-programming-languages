using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JourneyExpense
{
    /// <summary>
    /// Логика взаимодействия для StaticForm.xaml
    /// </summary>
    public partial class StaticForm : Window
    {
        public StaticForm()
        {
            InitializeComponent();
            DrawGraph();
        }

        private void DrawGraph()
        {
            Canvas canvas = new Canvas();
            canvas.Width = 400;
            canvas.Height = 400;
            double minX = -10;
            double maxX = 10;
            double minY = -10;
            double maxY = 10;
            double stepSize = 0.1;
            DrawAxes(canvas, minX, maxX, minY, maxY);
            DrawGraphPoints(canvas, minX, maxX, minY, maxY, stepSize);
            DrawGraphPointsRight(canvas, minX, maxX, minY, maxY, stepSize);
            AddFormulaText(canvas);
            Content = canvas;
        }

        private void DrawAxes(Canvas canvas, double minX, double maxX, double minY, double maxY)
        {
            double canvasWidth = canvas.Width;
            double canvasHeight = canvas.Height;

            Line xAxis = new Line();
            xAxis.Stroke = Brushes.Black;
            xAxis.StrokeThickness = 1;
            xAxis.X1 = 0;
            xAxis.Y1 = MapYCoordinate(canvasHeight, minY, maxY, 0);
            xAxis.X2 = canvasWidth;
            xAxis.Y2 = MapYCoordinate(canvasHeight, minY, maxY, 0);
            canvas.Children.Add(xAxis);

            Line yAxis = new Line();
            yAxis.Stroke = Brushes.Black;
            yAxis.StrokeThickness = 1;
            yAxis.X1 = MapXCoordinate(canvasWidth, minX, maxX, 0);
            yAxis.Y1 = 0;
            yAxis.X2 = MapXCoordinate(canvasWidth, minX, maxX, 0);
            yAxis.Y2 = canvasHeight;
            canvas.Children.Add(yAxis);

            for (double x = minX; x <= maxX; x += 1)
            {
                double canvasX = MapXCoordinate(canvasWidth, minX, maxX, x);
                double canvasY = MapYCoordinate(canvasHeight, minY, maxY, 0);

                Line tick = new Line();
                tick.Stroke = Brushes.Black;
                tick.StrokeThickness = 1;
                tick.X1 = canvasX;
                tick.Y1 = canvasY - 5;
                tick.X2 = canvasX;
                tick.Y2 = canvasY + 5;
                canvas.Children.Add(tick);

                TextBlock label = new TextBlock();
                label.Text = x.ToString();
                Canvas.SetLeft(label, canvasX - 10);
                Canvas.SetTop(label, canvasY + 10);
                canvas.Children.Add(label);
            }

            for (double y = minY; y <= maxY; y += 1)
            {
                double canvasX = MapXCoordinate(canvasWidth, minX, maxX, 0);
                double canvasY = MapYCoordinate(canvasHeight, minY, maxY, y);
                Line tick = new Line();
                tick.Stroke = Brushes.Black;
                tick.StrokeThickness = 1;
                tick.X1 = canvasX - 5;
                tick.Y1 = canvasY;
                tick.X2 = canvasX + 5;
                tick.Y2 = canvasY;
                canvas.Children.Add(tick);
                TextBlock label = new TextBlock();
                label.Text = y.ToString();
                Canvas.SetLeft(label, canvasX + 10);
                Canvas.SetTop(label, canvasY - 10);
                canvas.Children.Add(label);
            }
        }

        private void DrawGraphPoints(Canvas canvas, double minX, double maxX, double minY, double maxY, double stepSize)
        {
            for (double x = minX; x <= maxX; x += stepSize)
            {
                double y = Math.Abs(Math.Pow(x, 2) - 4 * x + 3);
                double canvasX = MapXCoordinate(canvas.Width, minX, maxX, x);
                double canvasY = MapYCoordinate(canvas.Height, minY, maxY, y);
                Ellipse point = new Ellipse();
                point.Fill = Brushes.Red;
                point.Width = 3;
                point.Height = 3;
                Canvas.SetLeft(point, canvasX - point.Width / 2);
                Canvas.SetTop(point, canvasY - point.Height / 2);
                canvas.Children.Add(point);
            }
        }
        private void DrawGraphPointsRight(Canvas canvas, double minX, double maxX, double minY, double maxY, double stepSize)
        {
            double canvasWidth = canvas.Width;
            double canvasHeight = canvas.Height;
            double offsetX = canvasWidth / 5;
            for (double x = minX; x <= maxX; x += stepSize)
            {
                double y = Math.Abs(Math.Pow(x, 2) - 4 * x + 3);
                double canvasX = MapXCoordinate(canvasWidth, minX, maxX, x) + offsetX;
                double canvasY = MapYCoordinate(canvasHeight, minY, maxY, y);
                Ellipse point = new Ellipse();
                point.Fill = Brushes.Green;
                point.Width = 3;
                point.Height = 3;
                Canvas.SetLeft(point, canvasX - point.Width / 2);
                Canvas.SetTop(point, canvasY - point.Height / 2);
                canvas.Children.Add(point);
            }
        }
        private void AddFormulaText(Canvas canvas)
        {
            TextBlock formulaText = new TextBlock();
            formulaText.Text = "y = |x^2 - 4x + 3|";
            formulaText.FontSize = 14;
            Canvas.SetLeft(formulaText, 10);
            Canvas.SetTop(formulaText, 10);
            canvas.Children.Add(formulaText);
        }
        private double MapXCoordinate(double canvasWidth, double minX, double maxX, double x)
        {
            return (x - minX) * canvasWidth / (maxX - minX);
        }
        private double MapYCoordinate(double canvasHeight, double minY, double maxY, double y)
        {
            return canvasHeight - (y - minY) * canvasHeight / (maxY - minY);
        }
    }
}
