using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class TextFileModel
    {
        public string FileName { get; set; }
        public string FileText { get; set; }
        public string FilePath { get; set; }

        //Text file object that holds the name text and path of a file
        public TextFileModel(string path, string name, string text)
        {
            this.FilePath = path;
            this.FileName = name;
            this.FileText = text; 
        }


    }


}
