using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WPFOperator.Views.CardViews
{
    /// <summary>
    /// Логика взаимодействия для CardManagerView.xaml
    /// </summary>
    public partial class CardManagerView : Window
    {
        public CardManagerView()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            CardListView CLV = new CardListView();
            CLV.DataContext = DataContext;
            CLV.Show();
        }

        private void MasterTransferTo_Click(object sender, RoutedEventArgs e)
        {
            DateTime? dt = CalendarTransfer.SelectedDate;

            if (dt != null)
            {
                MainViewModel MVM = (MainViewModel)DataContext;
                MVM.MasterTransfer(0, dt.Value);
            }
            else if (MessageBox.Show("Дата не выбрана, выбрать сегодняшний день?", "Alert", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {

            }
        }

        private void MasterTransferFrom_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
