using System;
using System.IO;
using FileLogger;
using Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using ViewConsole;
using ViewModel;

namespace LogTest
{
    [TestClass]
    public class LogTest
    {
        [TestMethod]
        public void LogTest_CheckFile()
        {
            Mock<TreeViewModel> vmTest = new Mock<TreeViewModel>();
            vmTest.SetupAllProperties();

            string path = "..\\..\\..\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
            Reflector reflector = new Reflector(path);

            vmTest.Object.Logger = new Logger();
            vmTest.Object.FilePathProvider = new BrowseFile();
            vmTest.Object.PathVariable = path;
            vmTest.Object.Reflector = reflector;

            vmTest.Object.LoadDLL();
            string pathToLog = "..\\..\\..\\Logs\\LogFile.log";
            if (File.Exists(path))
                Assert.IsTrue(File.Exists(pathToLog));
        }
    }
}
