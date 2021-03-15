using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FileSearch
{
    class SimpleCommand : ICommand
    {
        readonly Action onCommand;
        public SimpleCommand(Action onCommand) => this.onCommand = onCommand;

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => onCommand();
    }
}
