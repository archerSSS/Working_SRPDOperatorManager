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
    /// Логика взаимодействия для EmployerAddView.xaml
    /// </summary>
    public partial class EmployerAddView : Window
    {
        public EmployerAddView()
        {
            InitializeComponent();
        }


        public bool IsChildWindowClosed
        {
            get { return (bool)GetValue(IsChildWindowClosedProperty); }
            set { SetValue(IsChildWindowClosedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChildWindowClosed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsChildWindowClosedProperty =
            DependencyProperty.Register("IsChildWindowClosed", typeof(bool), typeof(EmployerAddView), new PropertyMetadata(false));

        

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (TextName.Text != "")
            {
                ((MainViewModel)DataContext).AddNewEmployer(TextName.Text);
                Close();
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            //Return();
            Close();
        }

        private void Return()
        {
            /*EmployerListView ELV = new EmployerListView();
            ELV.DataContext = DataContext;
            Close();
            ELV.Show();*/
        }

        protected override void OnClosed(EventArgs e)
        {
            //((MainViewModel)DataContext).ChildWindowCommand.Execute(null);
        }
    }
}
