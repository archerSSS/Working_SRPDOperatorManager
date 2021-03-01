using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFOperator.Models
{
    [Serializable]
    class CardActionsHandler
    {
        public List<DateTime> AddTimes;
        public List<DateTime> RemoveTimes;
        public List<DateTime> ReturnTimes;

        public CardActionsHandler()
        {
            AddTimes = new List<DateTime>();
            RemoveTimes = new List<DateTime>();
            ReturnTimes = new List<DateTime>();
        }

        public void DeleteAddAction(DateTime date)
        {
            foreach (DateTime dt in AddTimes)
            {
                if (dt.Year == date.Year && dt.Month == date.Month && dt.Day == date.Day)
                {
                    date = dt;
                    break;
                }
            }
            AddTimes.Remove(date);
        }
    }
}
