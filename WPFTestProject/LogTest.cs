using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using ViewModel;

namespace WPFTestProject
{
    [TestClass]
    public class LogTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Mock<TreeViewModel> vmTest = new Mock<TreeViewModel>();
            vmTest.SetupAllProperties();

            string path = "..\\..\\..\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
            Reflector reflector = new Reflector(path);

            vmTest.Object.PathVariable = path;
            vmTest.Object.Reflector = reflector;

            vmTest.Object.LoadDLL();
            string pathToLog = "..\\..\\..\\Logs\\LogFile.log";
            if (File.Exists(path))
                Assert.IsTrue(File.Exists(pathToLog));
        }
    }
}
