using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Assignment2
{
    public class DeleteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private TextBox myTextBox;
        private TextFileViewModel viewModel;


        public DeleteCommand(TextFileViewModel tfvm , TextBox textFileBox)
        {
            viewModel = tfvm;
            myTextBox = textFileBox;
        }


        public bool CanExecute(object parameter)
        {
            return viewModel.canDelete;
        }

        public void Execute(object parameter)
        {
            //Delete current selected file
            viewModel.DeleteFile();

            //Set everything to empty
            viewModel.SelectedFile = new TextFileModel("", "", "");

            viewModel.canEdit = false;
            viewModel.canSave = false;

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


