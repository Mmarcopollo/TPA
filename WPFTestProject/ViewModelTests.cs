using System;
using System.Threading;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using ViewModel;

namespace WPFTestProject
{
    [TestClass]
    public class ViewModelTests
    {
        [TestMethod]
        public void LoadDll_Loadingfile_CheckTimeOfLoading()
        {

            Mock<TreeViewModel> vmTest= new Mock<TreeViewModel>();
            vmTest.SetupAllProperties();

            string path = "..\\..\\..\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
            Reflector reflector = new Reflector(path);

            vmTest.Object.PathVariable = path;
            vmTest.Object.Reflector = reflector;

            vmTest.Object.LoadDLL();
            Thread.Sleep(3000);
            Assert.IsTrue(vmTest.Object.HierarchicalAreas.Count > 0);

        }

    }
}
