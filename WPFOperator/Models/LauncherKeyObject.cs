using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFOperator.Models
{
    [Serializable]
    class LauncherKeyObject
    {
        public DateTime date;

        public bool CheckUpdated()
        {
            return date > DateTime.Now;
        }
    }
}
