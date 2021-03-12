using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFOperator.Models
{
    class CardManager : INotifyPropertyChanged
    {
        private ObservableCollection<CardObject> handledCards;
        public ObservableCollection<CardObject> HandledCards { get { return handledCards; } set { handledCards = value; OnPropertyChanged("HandledCards"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string pn)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(pn));
            }
        }
    }
}
