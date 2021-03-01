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
using WPFOperator.Models;
using WPFOperator.ViewModels;
using WPFOperator.Views.CardViews;

namespace WPFOperator.Views.EmployerViews
{
    /// <summary>
    /// Логика взаимодействия для EmployerUpdateView.xaml
    /// </summary>
    public partial class EmployerUpdateView : Window
    {
        public EmployerUpdateView()
        {
            InitializeComponent();
        }

        private void EmployerUpdate_Click(object sender, RoutedEventArgs e)
        {
            Return();
        }

        private void CardAddView_Click(object sender, RoutedEventArgs e)
        {
            CardAddView CAV = new CardAddView();
            CAV.DataContext = DataContext;
            Close();
            CAV.Show();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            ((MainViewModel)DataContext).RestoreEmployerName();
            Return();
        }

        private void Return()
        {
            //EmployerListView ELV = new EmployerListView();
            //ELV.DataContext = DataContext;
            Close();
            //ELV.Show();
        }
    }
}
