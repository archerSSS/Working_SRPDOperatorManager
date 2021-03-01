using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFOperator.Commands
{
    class RelayCommand : ICommand
    {
        private Action<object> action;
        private Func<object, bool> func;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> a, Func<object, bool> f)
        {
            action = a;
            func = f;
        }

        public bool CanExecute(object parameter)
        {
            return func(parameter);
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }
    }
}
