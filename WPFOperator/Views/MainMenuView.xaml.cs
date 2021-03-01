using System;
using System.Collections.Generic;
using System.IO;
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
using WPFOperator.Views.CardViews;

namespace WPFOperator.Views
{
    /// <summary>
    /// Логика взаимодействия для MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : Window
    {
        public MainMenuView()
        {
            InitializeComponent();
            
        }

        public void CloseWindow(Window w)
        {
            w = null;
        }

        public void SetMainImage()
        {
            
        }

        private void EmployerView_Click(object sender, RoutedEventArgs e)
        {
            EmployerListView ELV = new EmployerListView();
            ELV.DataContext = DataContext;
            ELV.Show();
        }

        private void CardView_Click(object sender, RoutedEventArgs e)
        {
            CardListView CLV = new CardListView();
            CLV.DataContext = DataContext;
            string appLocation = Directory.GetCurrentDirectory();
            //CLV.MTL.Source = new Uri(appLocation + "/Media/pmr.mp4");
            CLV.Show();
        }

        private void OperationView_Click(object sender, RoutedEventArgs e)
        {
            ActionListView ALV = new ActionListView();
            ALV.DataContext = DataContext;
            ALV.Show();
        }

        private void AddCardType_Click(object sender, RoutedEventArgs e)
        {
            CardAddTypeView CATV = new CardAddTypeView();
            CATV.DataContext = DataContext;
            CATV.Show();
        }

        protected override void OnClosed(EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
