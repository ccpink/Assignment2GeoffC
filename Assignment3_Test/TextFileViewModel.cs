using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Search;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using System.IO;

namespace Assignment2
{
   
    public class TextFileViewModel : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static StorageFolder textfileFolder = ApplicationData.Current.LocalFolder;

        //private ObservableCollection<TextFileModel> files = new ObservableCollection<TextFileModel>();
        //public ObservableCollection<TextFileModel> Files { get{ return this.files; } }

        public ObservableCollection<TextFileModel> Files { get; set; }
        public List<TextFileModel> _allFiles = new List<TextFileModel>();


        private string _filter;

        public string FileText;
        public string FileName;
        public string FilePath;
        public bool isNew = true;
    
        private TextFileModel _selectedFile;


       
        public bool canEdit { get; set; } =  true;
        public bool canSave { get; set; } = true;
        public bool canDelete { get; set; } = false;

       

        public AddCommand AddCommand { get; }
        public EditCommand EditCommand { get; }
        public DeleteCommand DeleteCommand { get; }
        public SaveCommand SaveCommand { get; }
        public ExitCommand ExitCommand { get; }

        private TextBox textBox;



        public TextFileModel SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                _selectedFile = value;
                //If the file is empty
                if (value == null)
                { //Ouput that its empty

                }
                else //Set its text to the files text
                {
                    FileText = value.FileText;
                    FileName = value.FileName;
                    FilePath = value.FilePath;
                    canDelete = true;
                    canEdit = true;
                    canSave = false;
                    textBox.IsReadOnly = true;
                    isNew = false;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FileText"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FileName"));
                changesMade();
            }
        }

     

        public void changesMade() 
        {
            //Fire all the can executes changed
            DeleteCommand.FireCanExecuteChanged();
            AddCommand.FireCanExecuteChanged();
            EditCommand.FireCanExecuteChanged();
            SaveCommand.FireCanExecuteChanged();
            ExitCommand.FireCanExecuteChanged();

        }


        public TextFileViewModel(TextBox tfb)
        {
            //add the textbox and 
            textBox = tfb;
            
            //Create the colelction
            Files = new ObservableCollection<TextFileModel>();

            //Create the commands
            this.DeleteCommand = new DeleteCommand(this, tfb);
            this.AddCommand = new AddCommand(this, tfb);
            this.EditCommand = new EditCommand(this, tfb);
            this.SaveCommand = new SaveCommand(this, tfb);
            this.ExitCommand = new ExitCommand();


            //Create the collection
            CreateCollection();

            //Perform Filtering
            PerformFiltering();
        }



        //Make a collection of all files
        public async void CreateCollection()
        {
            _allFiles.Clear();
            Files.Clear();

            StorageFolder textFolder = ApplicationData.Current.LocalFolder;

            IReadOnlyList<StorageFile> storageFiles = await textFolder.GetFilesAsync();

            //For each file input into all files list
            foreach (StorageFile file in storageFiles)
            {
                //If file is null don't do anything
                if (file != null) { 
                    //this.Files.Add(new TextFileModel(file.Path, file.DisplayName, text));
                    try { 

                        _allFiles.Add(new TextFileModel(file.Path, file.DisplayName, await FileIO.ReadTextAsync(file)));
                     }
                    catch (FileNotFoundException e)
                    {

                    }
                }
            }

            PerformFiltering();
        }


        //Create a new file
        public async Task CreateNewFile(string fileName, string text)
        {
            Windows.Storage.StorageFile sampleFile = await textfileFolder.CreateFileAsync((fileName + ".txt"),
                Windows.Storage.CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(sampleFile, text);
        }

        //Create a new file
        public async Task CreateNewFile(string fileName)
        {
            Windows.Storage.StorageFile sampleFile = await textfileFolder.CreateFileAsync((fileName + ".txt"),
                Windows.Storage.CreationCollisionOption.ReplaceExisting);

            await Windows.Storage.FileIO.WriteTextAsync(sampleFile, "");
        }

        //Write to an existing file
        public async Task WriteToFile(string text)
        {
            Windows.Storage.StorageFile sampleFile =
                await textfileFolder.GetFileAsync(FileName + ".txt");

            await Windows.Storage.FileIO.WriteTextAsync(sampleFile, text);
        }

        //Delete a file
        public async Task DeleteFile()
        {
            
            Windows.Storage.StorageFile sampleFile =
                await textfileFolder.GetFileAsync(FileName + ".txt");

            await sampleFile.DeleteAsync();
            
        }


        public void PerformFiltering()
        {
            Files.Clear();

            //If filter is null set it to ""
            if (_filter == null)
            {
                _filter = "";
            }
            //If _filter has a value (ie. user entered something in Filter textbox)
            //Lower-case and trim string
            var lowerCaseFilter = Filter.ToLowerInvariant().Trim();

            //Use LINQ query to get all personmodel names that match filter text, as a list
            var result =
                _allFiles.Where(d => d.FileName.ToLowerInvariant()
                .Contains(lowerCaseFilter))
                .ToList();

            //Get list of values in current filtered list that we want to remove
            //(ie. don't meet new filter criteria)
            var toRemove = Files.Except(result).ToList();

            //Loop to remove items that fail filter
            foreach (var x in toRemove)
            {
                Files.Remove(x);
            }

            var resultCount = result.Count;
            
            // Add back in correct order.
            for (int i = 0; i < resultCount; i++)
            {
                var resultItem = result[i];
                if (i + 1 > Files.Count || !Files[i].Equals(resultItem))
                {
                    Files.Insert(i, resultItem);
                }
            }
        }

        //Getter and setter for Filter 
        public string Filter
        {
            get { return _filter; }
            set
            {
                if (value == _filter) { return; }
                _filter = value;
                PerformFiltering();
                //Invovoked whenever the property is changed
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));
            }
        }


        public bool NameExists(string text)
        {
            //For each file check if the name is there if it is set true else false
            foreach (TextFileModel file in Files)
            {
                if(text == file.FileName)
                {
                    return true;
                }
            }

            return false;
        }

        public bool isTrue;
        public async Task CanGetFile(string fileName)
        {

            try
            {

                Windows.Storage.StorageFile sampleFile =
                    await textfileFolder.GetFileAsync(fileName + ".txt");

                isTrue = true;
            }
            catch (FileNotFoundException)
            {
                isTrue = false;
            }


        }

    }
}
