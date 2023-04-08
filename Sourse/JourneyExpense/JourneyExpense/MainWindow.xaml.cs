using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JourneyExpense
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            initComboBox();
        }
        public static Dictionary<string, string> TypeFuel = new Dictionary<string, string>()
        {
            {"АИ-92","AI92"},
            {"АИ-95","AI95"},
            {"АИ-98","AI98"},
            {"ДТ","DT"},
        };
        public static List<string> TypeConsuption = new List<string> {"Городской","По трассе","Смешанный"};
        public void initComboBox()
        {
            for(int i = 0;i < TypeFuel.Count;i++)
            {
                comboBoxFuelType.Items.Add(TypeFuel.Keys.ElementAt(i));
            }
            foreach(var item in TypeConsuption)
            {
                comboBoxConsumption.Items.Add(item);
            }
        }
    }
}
