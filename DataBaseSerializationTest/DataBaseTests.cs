using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Database;
using Database.DTO;
using FileLogger;
using MEF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using ViewModel;
using ViewWPF;

namespace DataBaseSerializationTest
{
    [TestClass]
    public class DataBaseTests
    {
      [TestMethod]
        public void AssemblyDatabaseTest()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                db.AssemblyMetadata.Add(new AssemblyMetadataDatabaseDTO());
                Assert.IsNotNull(db.AssemblyMetadata.ToList());
            }
        }

        [TestMethod]
        public void NamespaceDatabaseTest()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                db.NamespaceMetadata.Add(new NamespaceMetadataDatabaseDTO());
                Assert.IsNotNull(db.AssemblyMetadata.ToList());
            }
        }

        [TestMethod]
        public void MethodDatabaseTest()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                db.MethodMetadata.Add(new MethodMetadataDatabaseDTO());
                Assert.IsNotNull(db.AssemblyMetadata.ToList());
            }
        }

        [TestMethod]
        public void ParameterDatabaseTest()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                db.ParameterMetadata.Add(new ParameterMetadataDatabaseDTO());
                Assert.IsNotNull(db.AssemblyMetadata.ToList());
            }
        }
        [TestMethod]
        public void PropertyDatabaseTest()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                db.PropertyMetadata.Add(new PropertyMetadataDatabaseDTO());
                Assert.IsNotNull(db.AssemblyMetadata.ToList());
            }
        }
        [TestMethod]
        public void TypeDatabaseTest()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                db.TypeMetadata.Add(new TypeMetadataDatabaseDTO());
                Assert.IsNotNull(db.AssemblyMetadata.ToList());
            }
        }

        [TestMethod]
        public void FieldDatabaseTest()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                db.FieldMetadata.Add(new FieldMetadataDatabaseDTO());
                Assert.IsNotNull(db.AssemblyMetadata.ToList());
            }
        }


        
        [TestMethod]
        public void DBSerializationTest()
        {
            Compose(this);

            string path = "..\\..\\..\\MyLibrary\\TPA.ApplicationArchitecture.dll";
            Mock<Reflector> reflector = new Mock<Reflector>(path);
            reflector.SetupAllProperties();

            Reflector baseReflector = new Reflector(path);
          
            DatabaseSerializer db = new DatabaseSerializer();
            reflector.Object.Serialization = db;
            reflector.Object.SerializeAssembly(path);

            reflector.Object.DeserializeAssembly(path);

            AssemblyMetadata test = new AssemblyMetadata(reflector.Object.M_AssemblyModel);

            Assert.AreEqual(test.Namespaces.ToList().Count(), baseReflector.M_AssemblyModel.Namespaces.Count());
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
