using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using MEF;
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
            Compose(this);
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

            Compose(this);
            Reflector reflector = new Reflector(pathDLL);

            Serializer repository = new Serializer();
            repository.Write(new AssemblyMetadataDTO(reflector.M_AssemblyModel), pathXML);

            AssemblyMetadataDTO assemblyLoaded = (AssemblyMetadataDTO)repository.Read(pathXML);

            Assert.AreEqual(reflector.M_AssemblyModel.Name, assemblyLoaded.Name);
        }

        [TestMethod]
        public void Seralizer_CheckEqualityOfData()
        {
            Compose(this);

            Reflector reflector = new Reflector(pathDLL);

            AssemblyMetadataDTO dto = new AssemblyMetadataDTO(reflector.M_AssemblyModel);

            AssemblyMetadata assembly = new AssemblyMetadata(dto);

            Assert.AreEqual(reflector.M_AssemblyModel.Name, assembly.Name);
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
