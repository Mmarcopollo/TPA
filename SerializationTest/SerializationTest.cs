using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Serialization;

namespace SerializationTest
{
    [TestClass]
    public class SerializationTest
    {
        private readonly string pathDLL = "..\\..\\..\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
        private readonly string pathXML = "..\\..\\..\\MyLibrary\\bin\\Debug\\Data.xml";
        [TestMethod]
        public void Seralizer_CheckExistanceOfFile()
        {

            Reflector reflector = new Reflector(pathDLL);

            Serializer repository = new Serializer();
            AssemblyMetadataDTO DTO = reflector.M_AssemblyModel.ConvertToDTO();
            repository.Write(DTO, pathXML);

            AssemblyMetadataDTO assemblyLoaded = repository.Read<AssemblyMetadataDTO>(pathXML);

            Assert.IsTrue(File.Exists("..\\..\\..\\MyLibrary\\bin\\Debug\\Data.xml"));
        }
        [TestMethod]
        public void Seralizer_LoadFile()
        {

            Reflector reflector = new Reflector(pathDLL);

            Serializer repository = new Serializer();
            repository.Write(reflector.M_AssemblyModel.ConvertToDTO(), pathXML);

            AssemblyMetadataDTO assemblyLoaded = repository.Read<AssemblyMetadataDTO>(pathXML);

            Assert.AreEqual(reflector.M_AssemblyModel.m_Name, assemblyLoaded.m_Name);
        }
    }
}
