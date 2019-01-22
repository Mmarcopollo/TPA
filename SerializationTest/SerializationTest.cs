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
            AssemblyMetadataDTO DTO = new AssemblyMetadataDTO(reflector.M_AssemblyModel);
            repository.Write(DTO, pathXML);

            AssemblyMetadataDTO assemblyLoaded = (AssemblyMetadataDTO)repository.Read(pathXML);

            Assert.IsTrue(File.Exists("..\\..\\..\\MyLibrary\\bin\\Debug\\Data.xml"));
        }
        [TestMethod]
        public void Seralizer_LoadFile()
        {

            Reflector reflector = new Reflector(pathDLL);

            Serializer repository = new Serializer();
            repository.Write(new AssemblyMetadataDTO(reflector.M_AssemblyModel), pathXML);

            AssemblyMetadataDTO assemblyLoaded = (AssemblyMetadataDTO)repository.Read(pathXML);

            Assert.AreEqual(reflector.M_AssemblyModel.Name, assemblyLoaded.Name);
        }

        [TestMethod]
        public void Seralizer_CheckEqualityOfData()
        {

            Reflector reflector = new Reflector(pathDLL);

            AssemblyMetadataDTO dto = new AssemblyMetadataDTO(reflector.M_AssemblyModel);

            AssemblyMetadata assembly = new AssemblyMetadata(dto);

            Assert.AreEqual(reflector.M_AssemblyModel.Name, assembly.Name);
        }


    
    }
}
