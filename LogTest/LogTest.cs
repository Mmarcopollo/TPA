using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using FileLogger;
using Log;
using MEF;
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
            Compose(this);
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


        public static void Compose(object obj)
        {
            CompositionContainer _container;
            AggregateCatalog _aggCatalog = new AggregateCatalog();

            _aggCatalog = new AggregateCatalog();
            DirectoryCatalog logger = new DirectoryCatalog("..\\..\\..\\FileLogger\\bin\\Debug");
            DirectoryCatalog serialize = new DirectoryCatalog("..\\..\\..\\Serialization\\bin\\Debug");
            DirectoryCatalog broser = new DirectoryCatalog("..\\..\\..\\WPFBrowseFile\\bin\\Debug");

            _aggCatalog.Catalogs.Add(logger);
            _aggCatalog.Catalogs.Add(serialize);
            _aggCatalog.Catalogs.Add(broser);

            _container = new CompositionContainer(_aggCatalog);
            _container.ComposeParts(obj);

            MefStartup.Instance._container = _container;

        }

    }
}
