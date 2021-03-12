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
using WPFOperator.Models;
using WPFOperator.ViewModels;
using WPFOperator.Views.EmployerViews;

namespace WPFOperator.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployerListView.xaml
    /// </summary>
    public partial class EmployerListView : Window
    {
        private bool OpeningChildWindow;

        public EmployerListView()
        {
            InitializeComponent();

            OpeningChildWindow = false;
        }


        private void EmployerAdd_Click(object sender, RoutedEventArgs e)
        {
            //IsChildWindowClosed = false;
            OpeningChildWindow = true;
            EmployerAddView EAV = new EmployerAddView();
            EAV.DataContext = DataContext;
            EAV.Show();
            Close();
            //ChildWindow = EAV;
        }

        private void EmployerUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (ComboEmployers.SelectedItem != null)
            {
                //IsChildWindowClosed = false;
                OpeningChildWindow = true;
                ((MainViewModel)DataContext).SetReservedEmployerName();
                EmployerUpdateView EUV = new EmployerUpdateView();
                EUV.DataContext = DataContext;
                EUV.Show();
                Close();
                //ChildWindow = EUV;
            }
        }

        private void EmployerRemove_Click(object sender, RoutedEventArgs e)
        {
            if (ComboEmployers.SelectedItem != null)
            {
                MainViewModel MVM = (MainViewModel)DataContext; 

                if (!MVM.IsEmployerFilled() && MessageBox.Show("Желаешь удалить сотрудника?", "Alert", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
                {
                    MVM.RemoveEmployer();
                    ComboEmployers.SelectedItem = null;
                }
                else
                {
                    MessageBox.Show("У этого сотрудника на руках еще есть карты.");
                }
            }
            
            
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnActivated(EventArgs e)
        {
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!OpeningChildWindow)
            {
                MainMenuView MMV = new MainMenuView();
                MMV.DataContext = DataContext;
                MMV.Show();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            
            /*if (ChildWindow != null) ChildWindow.Close();
            ((MainViewModel)DataContext).ChildWindowCommand.Execute(null);*/
        }
    }
}
