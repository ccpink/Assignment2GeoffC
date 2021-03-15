using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Assignment2
{
    public class EditCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private TextFileViewModel TFVM;
        private TextBox TFB;


        public EditCommand(TextFileViewModel tfvm , TextBox textFileBox)
        {
            TFVM = tfvm;
            TFB = textFileBox;
        }

        public bool CanExecute(object parameter)
        {
            return TFVM.canEdit;
        }

        public void Execute(object parameter)
        {
            //Set properties and allow them to edit contents of textbox
            TFB.IsReadOnly = false;
            TFVM.canEdit = false;
            TFVM.isNew = false;
            TFVM.canSave = true;
            TFVM.changesMade();
        }

        public void FireCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
