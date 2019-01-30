using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Database;
using Database.DTO;
using FileLogger;
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
            

            string path = "..\\..\\..\\MyLibrary\\bin\\Debug\\TPA.ApplicationArchitecture.dll";
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

    }
}
