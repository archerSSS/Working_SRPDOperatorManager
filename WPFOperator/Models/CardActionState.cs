using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFOperator.Models
{
    [Serializable]
    class CardActionState
    {
        private DateTime date;
        private int startCount;
        private int addedCount;
        private int returnedCount;
        private int removedCount;
        private int leftCount;

        public DateTime Date { get { return date; } set { date = value; } }
        public int StartCount { get { return startCount; } set { startCount = value; } }
        public int AddedCount { get { return addedCount; } set { addedCount = value; } }
        public int ReturnedCount { get { return returnedCount; } set { returnedCount = value; } }
        public int RemovedCount { get { return removedCount; } set { removedCount = value; } }
        public int LeftCount { get { return leftCount; } set { leftCount = value; } }

    }
}
