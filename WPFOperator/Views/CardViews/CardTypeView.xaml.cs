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
    /// Логика взаимодействия для CardTypeView.xaml
    /// </summary>
    public partial class CardTypeView : Window
    {
        public CardTypeView()
        {
            InitializeComponent();
        }

        private void AddTypeView_Click(object sender, RoutedEventArgs e)
        {
            //((MainViewModel)DataContext).AddNewType();

        }

        private void UpdateTypeView_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveType_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
