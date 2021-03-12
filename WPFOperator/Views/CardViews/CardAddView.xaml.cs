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
using WPFOperator.Views.EmployerViews;

namespace WPFOperator.Views.CardViews
{
    /// <summary>
    /// Логика взаимодействия для CardAddView.xaml
    /// </summary>
    public partial class CardAddView : Window
    {
        public CardAddView()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel MVM = (MainViewModel)DataContext;
            if (MVM.IsCardRemoved(TextNumber.Text))
            {
                MessageBox.Show("Такой номер карты уже был отдан клиенту однажды.");
                return;
            }

            if (MessageBox.Show("Хочешь чтобы я добавил новую карту этому сотруднику?", "Alert", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            if (TextNumber.Text != "" && ComboTypes.SelectedItem != null)
            {
                string n = TextNumber.Text;
                string t = (string)ComboTypes.SelectedItem;
                string employerName = ((MainViewModel)DataContext).IsCardExist(n);
                if (employerName != "")
                {
                    MessageBoxResult result = MessageBox.Show("Такая карта уже существует у сотрудника " + employerName + " либо был сдан им.\r Хочешь чтобы я передал эту карту выбранному сотруднику (" + ((MainViewModel)DataContext).Employer.FullName + ")?", "Alert", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                    {
                        DateTime? calendarDate = CalendarCard.SelectedDate;
                        DateTime dt = DateTime.Now;
                        if (calendarDate != null)
                        {
                            dt = calendarDate.Value;
                        }
                        else if (MessageBox.Show("Тобой не выбрана дата. \rЕсли нажмешь 'ДА', то я установлю сегодняшний день. \rЕсли нет, то я всё отменю.", "Alert", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

                        dt = new DateTime(dt.Year, dt.Month, dt.Day);
                        ((MainViewModel)DataContext).TransferCardFrom(n, employerName, dt);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    DateTime? calendarDate = CalendarCard.SelectedDate;
                    DateTime dt = DateTime.Now;
                    
                    if (calendarDate != null)
                    {
                        dt = calendarDate.Value;
                    }
                    else if (MessageBox.Show("Тобой не выбрана дата. \rЕсли нажмешь 'ДА', то я установлю сегодняшний день. \rЕсли нет, то я всё отменю.", "Alert", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

                    dt = new DateTime(dt.Year, dt.Month, dt.Day);
                    ((MainViewModel)DataContext).AddNewCard(n, t, dt);
                }
                
            }
        }

        // depricated
        private void CardTypeView_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Return()
        {
            Close();
        }

        // delete
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Хочешь чтобы я выдал эту карту клиенту? \r" + ListCards.SelectedItem, "Alert", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            if (ListCards.SelectedItem != null)
            {
                if (((MainViewModel)DataContext).IsCardReturned())
                {
                    MessageBox.Show("Невозможно выдать карту клиенту. Карта была возвращена.");
                }
                else if (((MainViewModel)DataContext).IsCardRemoved())
                {
                    MessageBox.Show("Невозможно выдать карту клиенту. Карта уже была выдана.");
                }
                else
                {
                    DateTime? calendarDate = CalendarCard.SelectedDate;
                    DateTime dt = DateTime.Now;
                    
                    if (calendarDate != null)
                    {
                        dt = calendarDate.Value;
                    }
                    else if (MessageBox.Show("Тобой не выбрана дата. \rЕсли нажмешь 'ДА', то я установлю сегодняшний день. \rЕсли нет, то я всё отменю.", "Alert", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

                    DateTime date = new DateTime(dt.Year, dt.Month, dt.Day);
                    ((MainViewModel)DataContext).RemoveCard(date);
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (ListCards.SelectedItem != null)
            {
                if (MessageBox.Show("Хочешь чтобы я забрал у сотрудника эту карту? \r" + ListCards.SelectedItem, "Alert", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

                if (((MainViewModel)DataContext).IsCardReturned())
                {
                    MessageBox.Show("Невозможно забрать эту карту. Карта уже была возвращена.");
                }
                else if (((MainViewModel)DataContext).IsCardRemoved())
                {
                    MessageBox.Show("Невозможно забрать эту карту. Карта была выдана клиенту.");
                }
                else
                {
                    DateTime? calendarDate = CalendarCard.SelectedDate;
                    DateTime dt = DateTime.Now;
                    
                    if (calendarDate != null)
                    {
                        dt = calendarDate.Value;
                    }
                    else if (MessageBox.Show("Тобой не выбрана дата. \rЕсли нажмешь 'ДА', то я установлю сегодняшний день. \rЕсли нет, то я всё отменю.", "Alert", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

                    DateTime date = new DateTime(dt.Year, dt.Month, dt.Day);
                    ((MainViewModel)DataContext).ReturnCard(date);
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ListCards.SelectedItem != null)
            {
                if (((MainViewModel)DataContext).DeleteCard())
                {
                    MessageBox.Show("Карта удалена!");
                }
                else MessageBox.Show("Невозможно удалить карту!");
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            CardListView CLV = new CardListView();
            CLV.DataContext = DataContext;
            CLV.Show();
        }
    }
}
