using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Assignment2
{
    public class AddCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private TextFileViewModel viewModel;
        private TextBox myTextBox;
    


        public AddCommand(TextFileViewModel tfvm, TextBox textFileBox)
        {
            viewModel = tfvm;
            myTextBox = textFileBox;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //Make it so you can save the file and you can no longer edit the file
            viewModel.SelectedFile = new TextFileModel("", "", "");

            //Make it so you can edit the text box
            myTextBox.IsReadOnly = false;
            viewModel.canSave = true;
            viewModel.canEdit = false;
            viewModel.isNew = true;

            viewModel.changesMade();
            viewModel.PerformFiltering();
            viewModel.CreateCollection();
        }

        public void FireCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
