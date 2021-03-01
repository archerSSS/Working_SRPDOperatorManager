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

namespace WPFOperator.Views.CardViews
{
    /// <summary>
    /// Логика взаимодействия для CardAddTypeView.xaml
    /// </summary>
    public partial class CardAddTypeView : Window
    {
        public CardAddTypeView()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (TextType.Text != "")
            {
                ((MainViewModel)DataContext).AddNewType(TextType.Text);
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            ((MainViewModel)DataContext).ChildWindowCommand.Execute(null);
        }
    }
}
