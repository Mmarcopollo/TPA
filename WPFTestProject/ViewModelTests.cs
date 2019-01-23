using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Threading;
using System.Windows.Controls;
using FileLogger;
using Log;
using MEF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using ViewModel;
using ViewWPF;
using WPFBrowseFile;

namespace WPFTestProject
{

    [TestClass]
    public class ViewModelTests
    {
        [TestMethod]
        public void LoadDll_Loadingfile_CheckTimeOfLoading()
        {
            Compose(this);

            Mock<TreeViewModel> vmTest= new Mock<TreeViewModel>();
            vmTest.SetupAllProperties();

            string path = "..\\..\\..\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
            Reflector reflector = new Reflector(path);

            vmTest.Object.Logger = new Logger();
            vmTest.Object.FilePathProvider = new BrowseFile();
            vmTest.Object.PathVariable = path;
            vmTest.Object.Reflector = reflector;

            vmTest.Object.LoadDLL();
            Thread.Sleep(3000);
            Assert.IsTrue(vmTest.Object.HierarchicalAreas.Count > 0);

        }

        [TestMethod]
        public void TreeViewLoaded_LoadingTree_CheckTimeOfLoading()
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

            vmTest.Object.TreeViewLoaded();
            Thread.Sleep(3000);
            Assert.IsTrue(vmTest.Object.HierarchicalAreas.Count > 0);

        }

        public void Compose(object obj)
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
