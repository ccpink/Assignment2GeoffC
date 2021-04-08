
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Assignment2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.Storage;

namespace Assignment3_Test
{
    [TestClass]
    public class UnitTest1
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        /// <summary>
        /// Overall testing for adding and fetching data
        /// Do tests one at a time...
        /// </summary>

        [TestMethod]
        public void TestDB()
        {
            DataRepo.InitializeDB();
            bool hasRecord = false;

            
            DataRepo.AddData("My Friends Name is Goof", "Jenny");

            DataRepo.AddData("My Friends Name is Goof", "Jess");

            DataRepo.AddData("My Friends Name is Goof", "Joey");
           

            List<TextFileModel> files = DataRepo.GetData();
            foreach (TextFileModel file in files)
            {
   
                string name = file.NoteName;
                string text = file.NoteText;
                int id = file.NoteID;
                TestContext.WriteLine(id + " " + text + " " + name );

                if (id == 3)
                {
                    if(text == "My Friends Name is Goof" && name == "Joey")
                    {
                        hasRecord = true;
                    }
                }

            }

            Assert.AreEqual(true, hasRecord);

        }


        /// <summary>
        /// Testing Update
        /// </summary>

        [TestMethod]
        public void TestUpdate()
        {
            bool hasRecord = false;

            DataRepo.UpdateData(2, "My new content");
            
            List<TextFileModel> files = DataRepo.GetData();

            foreach (TextFileModel file in files)
            {

                string name = file.NoteName;
                string text = file.NoteText;
                int id = file.NoteID;
                TestContext.WriteLine(id + " " + text + " " + name);

                if (id == 2)
                {
                    if (text == "My new content" && name == "Jess")
                    {
                        hasRecord = true;
                    }
                }

            }

            Assert.AreEqual(true, hasRecord);
        }

        /// <summary>
        /// Testing Delete.
        /// </summary>
        [TestMethod]
        public void TestDelete()
        {
            bool hasRecord = false;

            DataRepo.DeleteData(1);

            List<TextFileModel> files = DataRepo.GetData();
            foreach (TextFileModel file in files)
            {

                string name = file.NoteName;
                string text = file.NoteText;
                int id = file.NoteID;
                TestContext.WriteLine(id + " " + text + " " + name);

                if (id == 1)
                {
                    
                    hasRecord = true;
                    
                }

            }


            Assert.AreEqual(false, hasRecord);
        }


/*      Deprecated Tests
        
        [TestMethod]
        public void TestDelete1()
        {
            TextFileViewModel viewModel = new TextFileViewModel(null);
            
            string fileName = "JohnnyFile";

            viewModel.CreateNewFile(fileName, "This is Johnny's File").Wait();

            viewModel.FileName = fileName;

            viewModel.DeleteFile().Wait();

            viewModel.CanGetFile(fileName).Wait();

            Assert.AreEqual(false, viewModel.isTrue);

        }


        /// <summary>
        /// This one breaks if it is run as a group
        /// </summary>
        [TestMethod]
        public void TestDelete2()
        {
            //This is to test what happens if there isn't a file
            TextFileViewModel viewModel = new TextFileViewModel(null);

            string fileName = "JohnnyFile";

            viewModel.FileName = fileName;

            Assert.ThrowsException<AggregateException>(() => viewModel.DeleteFile().Wait());
        }


        [TestMethod]
        public void TestInsert()
        {
            //Check to see if file is there
            TextFileViewModel viewModel = new TextFileViewModel(null);

            string fileName = "JohnnyFile";

            viewModel.CreateNewFile(fileName, "This is Johnny's File").Wait();

            viewModel.CanGetFile(fileName).Wait();

            Assert.AreEqual(true, viewModel.isTrue);


        }

        [TestMethod]
        public void TestExists()
        {
            TextFileViewModel viewModel = new TextFileViewModel(null);

            string fileName = "JohnnyFile";

            viewModel.CreateNewFile(fileName, "This is Johnny's File").Wait();

            viewModel.CanGetFile(fileName).Wait();

            Assert.AreEqual(true, viewModel.isTrue);
        }

        */
    
    }
     
}

