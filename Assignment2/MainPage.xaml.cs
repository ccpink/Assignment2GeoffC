using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Data.SQLite;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Assignment2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Make the view model
        public TextFileViewModel viewModel { get; set; }


        public MainPage()
        {
           

            this.InitializeComponent();
         
            DataRepo.InitializeDB();
            //Make a new view model
            this.viewModel = new TextFileViewModel(fileText);
        }
        

        

        //I couldn't get rid of them without crashing the program multiple times
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
        }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage));
        }
    }
}
