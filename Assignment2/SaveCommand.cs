using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Assignment2
{
    public class SaveCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private TextFileViewModel viewModel;
        private TextBox myTextBox;

        public SaveCommand(TextFileViewModel tfvm, TextBox textFileBox)
        {
            viewModel = tfvm;
            myTextBox = textFileBox;
        }

        public bool CanExecute(object parameter)
        {
            return viewModel.canSave;
        }

        public async void Execute(object parameter)
        {
            //Text block text
            string txt = myTextBox.Text;

            //If the text is new
            if(viewModel.isNew){
                //Open a save dialog box
                SaveDialog saveDialog = new SaveDialog(viewModel);
                await saveDialog.ShowAsync();
                //If the result isn't empty then create a new file
                if(!saveDialog.Exited) { 
                    viewModel.CreateNewFile(saveDialog.Result, txt);
                }

            } else
            {
                //Write to an existing file and save the new content
                viewModel.WriteToFile(txt);
            }
            

            //Remake the collection of files and perform filtering
            viewModel.CreateCollection();
            viewModel.PerformFiltering();

            //Set properties
            viewModel.canEdit = false;
            viewModel.canDelete = false;
            viewModel.canSave = false;
            myTextBox.IsReadOnly = true;

            //Invoke changes
            viewModel.changesMade();

            //Set text box to blank
            myTextBox.Text = "";
        }

        public void FireCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}


