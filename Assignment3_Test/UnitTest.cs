
using System;
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
     

    }
}
