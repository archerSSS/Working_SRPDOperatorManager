using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFOperator.Models
{
    [Serializable]
    class CardAction
    {
        private DateTime Date { get; set; }
        private int ActionType { get; set; }

        public CardAction(DateTime d, int at)
        {
            Date = d;
            ActionType = at;
        }

        public int GetActionType()
        {
            return ActionType;
        }

        public DateTime GetDate()
        {
            return Date;
        }
    }
}
