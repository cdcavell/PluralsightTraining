using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest : TestBase
    {
        private const string BAD_FILE_NAME = @"C:\NotExists.bad";

        [TestMethod]
        public void FileNameDoesExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            SetGoodFileName();

            if (!string.IsNullOrEmpty(_GoodFileName)) {
                // Create the 'Good' file.
                File.AppendAllText(_GoodFileName, "Some Text");
            }

            TestContext.WriteLine("Checking File " + _GoodFileName);

            fromCall = fp.FileExists(_GoodFileName);

            // Delete file
            if (File.Exists(_GoodFileName)) {
                File.Delete(_GoodFileName);
            }

            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        public void FileNameDoesNotExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            TestContext.WriteLine("Checking File " + BAD_FILE_NAME);

            fromCall = fp.FileExists(BAD_FILE_NAME);

            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullOrEmpty_UsingAttribute()
        {
            FileProcess fp = new FileProcess();

            TestContext.WriteLine("Checking for a null File");

            fp.FileExists("");
        }

        [TestMethod]
        public void FileNameNullOrEmpty_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();

            try {
                TestContext.WriteLine("Checking for a null File");
                fp.FileExists("");
            }
            catch (ArgumentNullException) {
                // Test was a success
                return;
            }

            // Fail the test
            Assert.Fail("Call to FileExists() did NOT throw an ArgumentNullException.");
        }
    }
}
