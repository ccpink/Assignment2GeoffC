using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class TextFileModel
    {
        public string NoteName { get; set; }
        public string NoteText { get; set; }
        public string FilePath { get; set; }
        public int NoteID { get; set; }

        //Text file object that holds the name text and path of a file
        public TextFileModel(string path, string name, string text)
        {
            this.FilePath = path;
            this.NoteName = name;
            this.NoteText = text;
        }

        public TextFileModel(int ID, string name, string text)
        {
            this.NoteID = ID;
            this.NoteName = name;
            this.NoteText = text;
            this.FilePath = "";
        }


    }
}
