using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using WPFOperator.Commands;
using WPFOperator.Models;

namespace WPFOperator.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private string ReservedEmployerName;
        private string applicationDirection;
        private int currentSelectedBackground;
        private int collectedBits;
        private EmployerObject employer;
        private CardObject card;
        private CardManager cardMaster;
        private Dictionary<int, string> cardTypesDictionary;
        private ObservableCollection<string> cardTypesList;
        //private ObservableCollection<CardObject> cards;
        private ObservableCollection<CardObject> removedCards;
        private ObservableCollection<EmployerObject> employers;
        private ObservableCollection<CardActionState> monthlyActions;
        private SaveManager manager;
        private LauncherKeyObject key;
        private bool isChildWindowClosed;
        private ICommand childWindowCommand;
        private ICommand commandFileData;
        private ICommand commandPrintFileData;

        public string ApplicationDirection { get { return applicationDirection; } set { applicationDirection = value; OnPropertyChanged("ApplicationDirection"); } }
        public EmployerObject Employer { get { return employer; } set { employer = value; OnPropertyChanged("Employer"); } }
        public CardObject Card { get { return card; } set { card = value; OnPropertyChanged("Card"); } }
        public CardManager CardMaster { get { return cardMaster; } set { cardMaster = value; OnPropertyChanged("CardMaster"); } }
        //public ObservableCollection<CardObject> Cards { get { return cards; } set { cards = value; OnPropertyChanged("Cards"); } }
        public ObservableCollection<CardObject> RemovedCards { get { return removedCards; } set { removedCards = value; OnPropertyChanged("RemovedCards"); } }
        public ObservableCollection<string> CardTypesList { get { return cardTypesList; } set { cardTypesList = value; OnPropertyChanged("CardTypesList"); }  }
        public ObservableCollection<EmployerObject> Employers { get { return employers; } set { employers = value; OnPropertyChanged("Employers"); } }
        public ObservableCollection<CardActionState> MonthlyActions { get { return monthlyActions; } set { monthlyActions = value; OnPropertyChanged("MonthlyActions"); } }
        public bool IsChildWindowClosed { get { return isChildWindowClosed; } set { isChildWindowClosed = value; OnPropertyChanged("IsChildWindowClosed"); } }
        public ICommand ChildWindowCommand { get { if (childWindowCommand == null) childWindowCommand = new RelayCommand(SetChildWindowClosed, CanExecute); return childWindowCommand; } }
        public ICommand CommandFileData { get { if (commandFileData == null) commandFileData = new RelayCommand(CreateFileData, CanExecute); return commandFileData; } }
        public ICommand CommandPrintFileData { get { if (commandPrintFileData == null) commandPrintFileData = new RelayCommand(PrintFileData, CanExecute); return commandPrintFileData; } }


        public MainViewModel()
        {
            manager = new SaveManager();
            Employers = manager.LoadEmployers();
            RemovedCards = manager.LoadCardsArchive();
            cardTypesDictionary = manager.LoadTypes();
            cardMaster = new CardManager();
            key = manager.LoadKey();
            IsChildWindowClosed = true;

            cardTypesList = new ObservableCollection<string>();

            currentSelectedBackground = 0;
            ApplicationDirection = Directory.GetCurrentDirectory() + "/Media/" + currentSelectedBackground + ".mp4";
            // CARDS DISPOSING
            //Cards = new ObservableCollection<CardObject>();
            /*foreach (EmployerObject EO in Employers)
            {
                foreach (CardObject CO in EO.Cards)
                {
                    Cards.Add(CO);
                }
            }*/

            for (int i = 1; i < cardTypesDictionary.Count + 1; i++)
                cardTypesList.Add(cardTypesDictionary[i]);


            CardTypesList.Add("- - -");
        }

        public void SetReservedEmployerName()
        {
            ReservedEmployerName = Employer.FullName;
        }

        public void RestoreEmployerName()
        {
            Employer.FullName = ReservedEmployerName;
        }

        public void AddNewEmployer(string n)
        {
            EmployerObject EO = new EmployerObject();
            //EO.OnPropertyChanged("-");
            EO.Id = Employers.Count + 1;
            EO.FullName = n;
            Employers.Add(EO);

            SaveEmployers();
        }

        public void RemoveEmployer()
        {
            List<EmployerObject> ArchivedEmployers = manager.LoadEmployersArchive();

            Employers.Remove(Employer);
            ArchivedEmployers.Add(Employer);
            // CARDS DISPOSING
            /*foreach (CardObject CO in Employer.Cards) 
                Cards.Remove(CO);*/

            Employer = null;
            SaveEmployers();
            SaveEmployerArchive();
        }

        public void AddNewCard(string n, string t, DateTime dt)
        {
            CardObject CO = new CardObject() { CardType = t, Number = n, EmployerName = Employer.FullName };
            Employer.AddCard(CO, dt);
            //Cards.Add(CO); // CARDS DISPOSING

            SaveEmployers();
        }

        public void AddNewType(string t)
        {
            cardTypesDictionary.Add(cardTypesDictionary.Count + 1, t);
            cardTypesList.Clear();
            CardTypesList.Add("- - -");
            for (int i = 1; i < cardTypesDictionary.Count + 1; i++)
                cardTypesList.Add(cardTypesDictionary[i]);

            SaveTypes();
        }


        public void RemoveCard(DateTime date)
        {
            CardObject CO = Card;
            Employer.RemoveCard(Card, date);
            RemovedCards.Add(CO);
            //Card.RemoveCard(date);
            SaveEmployers();
            SaveCardArchive();
        }

        public void ReturnCard(DateTime date)
        {
            Employer.ReturnCard(Card, date);
            //Card.ReturnCard(date);
            SaveEmployers();
        }

        public bool DeleteCard()
        {
            CardObject CO = Card;
            if (Employer.DeleteCard(CO) /*&& Cards.Remove(CO)*/) // CARDS DISPOSING
            {
                Card = null;
                SaveEmployers();
                return true;
            }
            return false;
        }

        public void SelectEmployer(EmployerObject EO)
        {
            Employer = EO;
        }

        public void DeselectEmployer()
        {
            Employer = null;
        }

        public void SelectCard(CardObject CO)
        {
            Card = CO;
        }

        public void DeselectCard()
        {
            Card = null;
        }

        private void SetChildWindowClosed(object o)
        {
            IsChildWindowClosed = !isChildWindowClosed;
        }

        private void CreateFileData(object o)
        {
            string file = "";
            foreach (EmployerObject EO in Employers)
            {
                file += EO.FullName;

                int tabCount = 6;
                bool firstLine = true;
                for (int t = 0; t < tabCount - (EO.FullName.Length / 8); t++) file += "\t";

                foreach (CardObject CO in EO.HandledCards)
                {
                    if (!firstLine) for (int t = 0; t < tabCount; t++) file += "\t";
                    else firstLine = false;
                    file += CO.Number;
                    file += "\n";
                }
                file += "\r";
                firstLine = true;
            }

            FileInfo fi = new FileInfo("C:/BitFiles");
            Directory.CreateDirectory(fi.FullName);
            
            StreamWriter sw = new StreamWriter("C:/BitFiles/BitFile.txt"); //"E:/BitFiles/BitFile.txt"
            sw.WriteLine(file);
            sw.Close();
            

            //new DataPrinter();
        }

        private void PrintFileData(object o)
        {
            string file = "";
            foreach (EmployerObject EO in Employers)
            {
                file += EO.FullName;

                int tabCount = 6;
                bool firstLine = true;
                for (int t = 0; t < tabCount - (EO.FullName.Length / 8); t++) file += "\t";

                foreach (CardObject CO in EO.HandledCards)
                {
                    if (!firstLine) for (int t = 0; t < tabCount; t++) file += "\t";
                    else firstLine = false;
                    file += CO.Number;
                    file += "\n";
                }
                file += "\r";
                firstLine = true;
            }
            FileInfo fi = new FileInfo("C:/BitFiles");
            Directory.CreateDirectory(fi.FullName);

            StreamWriter sw = new StreamWriter("C:/BitFiles/BitFile.txt");

            sw.WriteLine(file);
            sw.Close();

            new DataPrinter();
        }

        private bool CanExecute(object o)
        {
            return true;
        }

        public string IsCardExist(string n)
        {
            foreach (EmployerObject EO in Employers)
            {
                foreach (CardObject CO in EO.HandledCards)
                {
                    if (CO.Number == n && CO.EmployerName != "")
                    {
                        return CO.EmployerName;
                    }
                }
            }

            // CARDS DISPOSING
            /*foreach (CardObject CO in Cards)
            {
                if (CO.Number == n && CO.EmployerName != "")
                {
                    return CO.EmployerName;
                }
            }*/

            return "";
        }

        public bool IsCardRemoved(string number)
        {
            foreach (CardObject CO in RemovedCards)
            {
                if (CO.Number == number) return true;
            }
            return false;
        }

        public bool IsEmployerFilled()
        {
            return Employer.HasCards();
        }

        public bool IsCardRemoved()
        {
            return Card.IsCardRemoved();
        }

        public bool IsCardReturned()
        {
            return Card.IsCardReturned();
        }

        public string GetCardNumber()
        {
            return Card.Number;
        }

        public void TransferCardFrom(string cardNumber, string EmployerFrom, DateTime date)
        {
            foreach (EmployerObject EO in Employers)
            {
                foreach (CardObject CO in EO.HandledCards)
                {
                    if (CO.Number == cardNumber)
                    {
                        Card = CO;
                        break;
                    }
                }
            }

            foreach (EmployerObject EOFrom in Employers)
            {
                if (EOFrom.FullName == EmployerFrom)
                {
                    if (!Card.IsCardReturned())
                    {
                        EOFrom.ReturnCard(Card, date);
                    }
                    
                    Employer.AddCardTransfer(Card, date);
                    Card.EmployerName = Employer.FullName;
                    SaveEmployers();
                    break;
                }
            }

            Card = null;
        }

        public void MasterTransfer(int mode, DateTime date)
        {
            //CardObject CO = (CardObject)card;
            int m = Int32.Parse(mode.ToString());

            
            if (m == 0) // Give to Employer
            {
                CardMaster.RemodeCard(Card);
                Employer.ReturnCard(Card, date);
                Employer.AddCardTransfer(Card, date);
                Card.EmployerName = Employer.FullName;
                SaveEmployers();
            }
            else if (m == 1) // back from Employer
            {

                //SaveEmployers();
            }
            
            //Card = null;
        }

        public void FormActionsData(DateTime bgn, DateTime end)
        {
            foreach (EmployerObject EO in Employers)
            {
                EO.GetActionsState(bgn, end);
            }
        }

        public void SaveEmployers()
        {
            manager.SaveEmployers(Employers);
        }

        public void SaveEmployerArchive()
        {
            
        }

        public void SaveCardArchive()
        {
            manager.SaveCardsArchive(RemovedCards);
        }

        public void SaveTypes()
        {
            manager.SaveCardTypes(cardTypesDictionary);
        }

        public void RemoveMonthlyActions()
        {
            MonthlyActions = null;
        }

        public bool IsKeyUptoDate()
        {
            return key.CheckUpdated();
        }


        public event PropertyChangedEventHandler PropertyChanged;


        public void OnPropertyChanged(string pn)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(pn));
            }
        }
    }
}
