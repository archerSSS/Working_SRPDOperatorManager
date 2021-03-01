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
using WPFOperator.ViewModels;

namespace WPFOperator.Views
{
    /// <summary>
    /// Логика взаимодействия для ActionListView.xaml
    /// </summary>
    public partial class ActionListView : Window
    {
        private int Year;
        private int Month;
        private int Day;

        public ActionListView()
        {
            InitializeComponent();
            DateTime dt = DateTime.Now;
            Year = dt.Year;
            Month = dt.Month;
            Day = dt.Day;
        }



        public string CurrentDate
        {
            get { return (string)GetValue(CurrentDateProperty); }
            set { SetValue(CurrentDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentDateProperty =
            DependencyProperty.Register("CurrentDate", typeof(string), typeof(ActionListView), new PropertyMetadata());



        private void Return_Click(object sender, RoutedEventArgs e)
        {
            //((MainViewModel)DataContext).RemoveMonthlyActions();
            MainMenuView MMV = new MainMenuView();
            MMV.DataContext = DataContext;
            MMV.SetMainImage();
            Close();
            MMV.Show();
        }

        private void AddYear_Click(object sender, RoutedEventArgs e)
        {
            Year++;
            CurrentDate = Year + "." + Month + "." + Day;
        }

        private void AddMonth_Click(object sender, RoutedEventArgs e)
        {
            Month++;
            if (Month > 12) Month = 1;
            CurrentDate = Year + "." + Month + "." + Day;
        }

        private void AddDay_Click(object sender, RoutedEventArgs e)
        {
            Day++;
            if (Day > 31) Day = 1;
            CurrentDate = Year + "." + Month + "." + Day;
        }

        private void RemoveYear_Click(object sender, RoutedEventArgs e)
        {
            Year--;
            CurrentDate = Year + "." + Month + "." + Day;
        }

        private void RemoveMonth_Click(object sender, RoutedEventArgs e)
        {
            Month--;
            if (Month < 1) Month = 12; 
            CurrentDate = Year + "." + Month + "." + Day;
        }

        private void RemoveDay_Click(object sender, RoutedEventArgs e)
        {
            Day--;
            if (Day < 1) Day = 31; 
            CurrentDate = Year + "." + Month + "." + Day;
        }

        private void FormActions_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
