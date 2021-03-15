using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Assignment2
{
    public class ExitCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
       
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {   //Exit program
            System.Environment.Exit(1);
        }

        public void FireCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

