using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Serialization;
using ViewModel;

namespace WPFTestProject
{
    [TestClass]
    public class SerializationTest
    {
        private string pathDLL = "..\\..\\..\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
        private string pathXML = "..\\..\\..\\MyLibrary\\bin\\Debug\\Data.xml";
        [TestMethod]
        public void Seralizer_CheckExistanceOfFile()
        {

            Reflector reflector = new Reflector(pathDLL);

            Serializer repozitory = new Serializer();
            repozitory.Write(reflector.M_AssemblyModel, pathXML);

            AssemblyMetadata assemblyLoaded = repozitory.Read(pathXML);

            Assert.IsTrue(File.Exists("..\\..\\..\\MyLibrary\\bin\\Debug\\Data.xml"));
        }
        [TestMethod]
        public void Seralizer_LoadFile()
        {

            Reflector reflector = new Reflector(pathDLL);

            Serializer repozitory = new Serializer();
            repozitory.Write(reflector.M_AssemblyModel, pathXML);

            AssemblyMetadata assemblyLoaded = repozitory.Read(pathXML);

            Assert.AreEqual(reflector.M_AssemblyModel.m_Name,assemblyLoaded.m_Name);
        }
    }
}
