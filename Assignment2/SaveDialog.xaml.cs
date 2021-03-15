using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Assignment2
{
    public sealed partial class SaveDialog : ContentDialog
    {
        TextFileViewModel viewModel;
        public string Result = "";
        public bool Exited = false;
        public SaveDialog(TextFileViewModel vm)
        {
            viewModel = vm;
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            //If its null or empty don't allow it to save
            if (string.IsNullOrEmpty(fileName.Text))
            {
                args.Cancel = true;
                errorBox.Text = "Empty name";

            }// IF NAME EXISTS DON'T SAVE
            else if (viewModel.nameExists(fileName.Text))
            {
                args.Cancel = true;
                errorBox.Text = "Name exists";
            }
            else
            { // ITS a fine name
                Result = fileName.Text;
                args.Cancel = false;
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {   //You exited the dialog box without entering a name
            this.Result = "";
            this.Exited = true;
        }
    }
}
