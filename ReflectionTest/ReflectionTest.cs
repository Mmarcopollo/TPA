using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace ReflectionTest
{
    [TestClass]
    public class ReflectionTest
    {
        [TestMethod]
        public void ReflectionTest_IsNumberOfElementsEqualToRealNumber()
        {
            string path = "..\\..\\..\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
            Reflector reflector = new Reflector(path);

            List<NamespaceMetadata> namespaces = ((IEnumerable<NamespaceMetadata>)reflector.M_AssemblyModel.Namespaces).ToList();
            Assert.AreEqual(namespaces.Count, 1);
            List<TypeMetadata> types = ((IEnumerable<TypeMetadata>)namespaces[0].Types).ToList();
            Assert.AreEqual(types.Count, 3);
            List<MethodMetadata> methods;
            List<PropertyMetadata> props;

            methods = ((IEnumerable<MethodMetadata>)types[0].Methods).ToList();
            props = ((IEnumerable<PropertyMetadata>)types[0].Properties).ToList();
            Assert.AreEqual(methods.Count, 8);
            Assert.AreEqual(props.Count, 2);

            methods = ((IEnumerable<MethodMetadata>)types[1].Methods).ToList();
            props = ((IEnumerable<PropertyMetadata>)types[1].Properties).ToList();
            Assert.AreEqual(methods.Count, 8);
            Assert.AreEqual(props.Count, 2);

            methods = ((IEnumerable<MethodMetadata>)types[2].Methods).ToList();
            props = ((IEnumerable<PropertyMetadata>)types[2].Properties).ToList();
            Assert.AreEqual(methods.Count, 9);
            Assert.AreEqual(props.Count, 2);

        }

        [TestMethod]
        public void NamespacemetadataTest_IsNumberOfElementsEqualToRealNumber()
        {
            string path = "..\\..\\..\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
            Reflector reflector = new Reflector(path);

            List<NamespaceMetadata> namespaces = ((IEnumerable<NamespaceMetadata>)reflector.M_AssemblyModel.Namespaces).ToList();
            Assert.AreEqual(namespaces.Count, 1);

        }

        [TestMethod]
        public void TypesMetadataTest_IsNumberOfElementsEqualToRealNumber()
        {
            string path = "..\\..\\..\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
            Reflector reflector = new Reflector(path);

            List<NamespaceMetadata> namespaces = ((IEnumerable<NamespaceMetadata>)reflector.M_AssemblyModel.Namespaces).ToList();
            List<TypeMetadata> types = ((IEnumerable<TypeMetadata>)namespaces[0].Types).ToList();
            Assert.AreEqual(types.Count, 3);

        }

        [TestMethod]
        public void MethodsMetadataTest_IsNumberOfElementsEqualToRealNumber()
        {
            string path = "..\\..\\..\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
            Reflector reflector = new Reflector(path);

            List<NamespaceMetadata> namespaces = ((IEnumerable<NamespaceMetadata>)reflector.M_AssemblyModel.Namespaces).ToList();
            Assert.AreEqual(namespaces.Count, 1);
            List<TypeMetadata> types = ((IEnumerable<TypeMetadata>)namespaces[0].Types).ToList();
            Assert.AreEqual(types.Count, 3);
            List<MethodMetadata> methods;

            methods = ((IEnumerable<MethodMetadata>)types[0].Methods).ToList();
            Assert.AreEqual(methods.Count, 8);

            methods = ((IEnumerable<MethodMetadata>)types[1].Methods).ToList();
            Assert.AreEqual(methods.Count, 8);

            methods = ((IEnumerable<MethodMetadata>)types[2].Methods).ToList();
            Assert.AreEqual(methods.Count, 9);

        }

        [TestMethod]
        public void PropertiesMetadataTest_IsNumberOfElementsEqualToRealNumber()
        {
            string path = "..\\..\\..\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
            Reflector reflector = new Reflector(path);

            List<NamespaceMetadata> namespaces = ((IEnumerable<NamespaceMetadata>)reflector.M_AssemblyModel.Namespaces).ToList();
            List<TypeMetadata> types = ((IEnumerable<TypeMetadata>)namespaces[0].Types).ToList();

            List<PropertyMetadata> props;

            props = ((IEnumerable<PropertyMetadata>)types[0].Properties).ToList();
            Assert.AreEqual(props.Count, 2);

            props = ((IEnumerable<PropertyMetadata>)types[1].Properties).ToList();
            Assert.AreEqual(props.Count, 2);

            props = ((IEnumerable<PropertyMetadata>)types[2].Properties).ToList();
            Assert.AreEqual(props.Count, 2);

        }
    }
}
