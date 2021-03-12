using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using WPFOperator.Models;
using WPFOperator.ViewModels;
using WPFOperator.Views.CardViews;

namespace WPFOperator.Views
{
    /// <summary>
    /// Логика взаимодействия для CardListView.xaml
    /// </summary>
    
    public partial class CardListView : Window
    {
        private int Year;
        private int Month;
        private int Day;

        private int YearActions;
        private int MonthActions;
        private int DayActions;

        private bool OpeningChildWindow;

        //private Window ChildWindow;

        public CardListView()
        {
            InitializeComponent();

            Cards = new ObservableCollection<CardObject>();
            DateTime dt = DateTime.Now;
            Year = dt.Year;
            Month = dt.Month;
            Day = dt.Day;
            CurrentDate = Year + "." + Month + "." + Day;

            OpeningChildWindow = false;
        }

        // Depricated
        /*public bool IsChildWindowClosed
        {
            get { return (bool)GetValue(IsChildWindowClosedProperty); }
            set { SetValue(IsChildWindowClosedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChildWindowClosed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsChildWindowClosedProperty =
            DependencyProperty.Register("IsChildWindowClosed", typeof(bool), typeof(CardListView), new PropertyMetadata(true));*/

        public string CurrentDate
        {
            get { return (string)GetValue(CurrentDateProperty); }
            set { SetValue(CurrentDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentDateProperty =
            DependencyProperty.Register("CurrentDate", typeof(string), typeof(CardListView), new PropertyMetadata());

        public string CurrentDateActions
        {
            get { return (string)GetValue(CurrentDateActionsProperty); }
            set { SetValue(CurrentDateActionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentDateActionsProperty =
            DependencyProperty.Register("CurrentDateActions", typeof(string), typeof(CardListView), new PropertyMetadata());


        ObservableCollection<CardObject> Cards
        {
            get { return (ObservableCollection<CardObject>)GetValue(CardsProperty); }
            set { SetValue(CardsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CardTypes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CardsProperty =
            DependencyProperty.Register("Cards", typeof(ObservableCollection<CardObject>), typeof(CardListView), new PropertyMetadata());



        private void CardAdd_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void CardUpdate_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CardRemove_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DataGridComboBoxColumn_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            
            
            
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void FilterCards()
        {
            MainViewModel MVM = (MainViewModel)DataContext;
            if (MVM.Employer != null)
            {
                Cards.Clear();
                if (ComboFilter.SelectedItem != null && (string)ComboFilter.SelectedItem != "- - -")
                {
                    string item = (string)ComboFilter.SelectedItem;
                    foreach (CardObject CO in MVM.Employer.Cards)
                    {
                        if (CO.CardType == item)
                        {
                            CardObject COView = new CardObject();
                            COView.Number = CO.Number;
                            COView.CardType = CO.CardType;
                            Cards.Add(COView);
                        }
                    }
                }
                else
                {
                    foreach (CardObject CO in MVM.Employer.Cards)
                    {
                        CardObject COView = new CardObject();
                        COView.Number = CO.Number;
                        COView.CardType = CO.CardType;
                        Cards.Add(COView);
                    }
                }
            }
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

        private void AddYearActions_Click(object sender, RoutedEventArgs e)
        {
            YearActions++;
            CurrentDate = YearActions + "." + MonthActions + "." + DayActions;
        }


        private void ListCards_Click(object sender, RoutedEventArgs e)
        {
            string dateBegin = TextDateBegin.Text;
            string dateEnd = TextDateEnd.Text;
            if (dateBegin.Length == 10 && dateEnd.Length == 10)
            {
                DateTime dateTimeBegin;
                DateTime dateTimeEnd;
                try
                {
                    dateTimeBegin = new DateTime(
                        Int32.Parse(dateBegin[0] + "" + dateBegin[1] + "" + dateBegin[2] + "" + dateBegin[3]),
                        Int32.Parse(dateBegin[5] + "" + dateBegin[6]),
                        Int32.Parse(dateBegin[8] + "" + dateBegin[9]));
                    dateTimeEnd = new DateTime(
                        Int32.Parse(dateEnd[0] + "" + dateEnd[1] + "" + dateEnd[2] + "" + dateEnd[3]),
                        Int32.Parse(dateEnd[5] + "" + dateEnd[6]),
                        Int32.Parse(dateEnd[8] + "" + dateEnd[9]));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Упс. Указан неверный формат даты.");
                    return;
                }


                MainViewModel MVM = (MainViewModel)DataContext;
                if (MVM.Employer != null)
                {
                    Cards.Clear();
                    if (ComboFilter.SelectedItem != null)
                    {
                        string item = (string)ComboFilter.SelectedItem;
                        DateTime selectedDate = new DateTime(Year, Month, Day);
                        foreach (CardObject CO in MVM.Employer.HandledCards)
                        {
                            if (CO.IsAddedBetween(dateTimeBegin, dateTimeEnd))
                            {
                                if (item == "- - -" || item == CO.CardType)
                                {
                                    CardObject COView = new CardObject();
                                    COView.Number = CO.Number;
                                    COView.CardType = CO.CardType;
                                    COView.EmployerName = CO.EmployerName;
                                    COView.Actions = CO.Actions;
                                    COView.AddedLastTime = CO.AddedLastTime;
                                    COView.RemovedLastTime = CO.RemovedLastTime;
                                    COView.ReturnedLastTime = CO.ReturnedLastTime;
                                    Cards.Add(COView);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (CardObject CO in MVM.Employer.HandledCards)
                        {
                            CardObject COView = new CardObject();
                            COView.Number = CO.Number;
                            COView.CardType = CO.CardType;
                            Cards.Add(COView);
                        }
                    }
                }
            }
        }
        private void ListFormActions_Click(object sender, RoutedEventArgs e)
        {
            string dateBegin = TextDateActionsBegin.Text;
            string dateEnd = TextDateActionsEnd.Text;
            if (dateBegin.Length == 10 && dateEnd.Length == 10)
            {
                DateTime dateTimeBegin;
                DateTime dateTimeEnd;
                try
                {
                    dateTimeBegin = new DateTime(
                        Int32.Parse(dateBegin[0] + "" + dateBegin[1] + "" + dateBegin[2] + "" + dateBegin[3]),
                        Int32.Parse(dateBegin[5] + "" + dateBegin[6]),
                        Int32.Parse(dateBegin[8] + "" + dateBegin[9]));
                    dateTimeEnd = new DateTime(
                        Int32.Parse(dateEnd[0] + "" + dateEnd[1] + "" + dateEnd[2] + "" + dateEnd[3]),
                        Int32.Parse(dateEnd[5] + "" + dateEnd[6]),
                        Int32.Parse(dateEnd[8] + "" + dateEnd[9]));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ай ай. Формат даты неверный.");
                    return;
                }

                MainViewModel MVM = (MainViewModel)DataContext;
                MVM.FormActionsData(dateTimeBegin, dateTimeEnd);
            }
        }

        private void NumbersEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DataEmployers.SelectedItem != null)
            {
                OpeningChildWindow = true;

                CardAddView CAV = new CardAddView();
                CAV.DataContext = DataContext;
                CAV.Show();
                Close();
            }
        }

        /*protected override void OnActivated(EventArgs e)
        {
            if (ChildWindow != null && !ChildWindow.IsVisible)
            {
                IsChildWindowClosed = true;
            }
        }*/

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!OpeningChildWindow)
            {
                MainMenuView MMV = new MainMenuView();
                MMV.DataContext = DataContext;
                MMV.Show();
            }
        }

        /*protected override void OnClosed(EventArgs e)
        {
            if (ChildWindow != null) ChildWindow.Close();
            ((MainViewModel)DataContext).ChildWindowCommand.Execute(null);
        }*/

        private void PrintFile_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void CreateFile_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}