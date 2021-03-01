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
    /// Логика взаимодействия для FirstWindow.xaml
    /// </summary>
    public partial class FirstWindow : Window
    {
        public FirstWindow()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            MainMenuView MMV = new MainMenuView();
            MainViewModel MVM = new MainViewModel();
            if (MVM.IsKeyUptoDate())
            {
                MMV.DataContext = MVM;
                MMV.SetMainImage();
                MMV.Show();
            }
            /*else
            {
                System.Environment.Exit(0);
            }*/
            //MMV.DataContext = MVM;
            Close();
            //MMV.Show();
        }
    }
}
