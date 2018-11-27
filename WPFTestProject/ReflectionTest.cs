using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace WPFTestProject
{
    [TestClass]
    public class ReflectionTest
    {
        [TestMethod]
        public void ReflectionTest_IsNumberOfElementsEqualToRealNumber()
        {
            string path = "..\\..\\..\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
            Reflector reflector = new Reflector(path);

            List<NamespaceMetadata> namespaces = reflector.M_AssemblyModel.m_Namespaces.ToList();
            Assert.AreEqual(namespaces.Count, 1);
            List<TypeMetadata> types = namespaces[0].m_Types.ToList();
            Assert.AreEqual(types.Count, 3);
            List<MethodMetadata> methods;
            List<PropertyMetadata> props;

            methods = types[0].m_Methods.ToList();
            props = types[0].m_Properties.ToList();
            Assert.AreEqual(methods.Count, 4);
            Assert.AreEqual(props.Count, 2);

            methods = types[1].m_Methods.ToList();
            props = types[1].m_Properties.ToList();
            Assert.AreEqual(methods.Count, 4);
            Assert.AreEqual(props.Count, 2);

            methods = types[2].m_Methods.ToList();
            props = types[2].m_Properties.ToList();
            Assert.AreEqual(methods.Count, 5);
            Assert.AreEqual(props.Count, 2);

        }

        [TestMethod]
        public void NamespacemetadataTest_IsNumberOfElementsEqualToRealNumber()
        {
            string path = "..\\..\\..\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
            Reflector reflector = new Reflector(path);

            List<NamespaceMetadata> namespaces = reflector.M_AssemblyModel.m_Namespaces.ToList();
            Assert.AreEqual(namespaces.Count, 1);

        }

        [TestMethod]
        public void TypesMetadataTest_IsNumberOfElementsEqualToRealNumber()
        {
            string path = "..\\..\\..\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
            Reflector reflector = new Reflector(path);

            List<NamespaceMetadata> namespaces = reflector.M_AssemblyModel.m_Namespaces.ToList();
            List<TypeMetadata> types = namespaces[0].m_Types.ToList();
            Assert.AreEqual(types.Count, 3);

        }

        [TestMethod]
        public void MethodsMetadataTest_IsNumberOfElementsEqualToRealNumber()
        {
            string path = "..\\..\\..\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
            Reflector reflector = new Reflector(path);

            List<NamespaceMetadata> namespaces = reflector.M_AssemblyModel.m_Namespaces.ToList();
            Assert.AreEqual(namespaces.Count, 1);
            List<TypeMetadata> types = namespaces[0].m_Types.ToList();
            Assert.AreEqual(types.Count, 3);
            List<MethodMetadata> methods;

            methods = types[0].m_Methods.ToList();
            Assert.AreEqual(methods.Count, 4);

            methods = types[1].m_Methods.ToList();
            Assert.AreEqual(methods.Count, 4);

            methods = types[2].m_Methods.ToList();
            Assert.AreEqual(methods.Count, 5);

        }

        [TestMethod]
        public void PropertiesMetadataTest_IsNumberOfElementsEqualToRealNumber()
        {
            string path = "..\\..\\..\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
            Reflector reflector = new Reflector(path);

            List<NamespaceMetadata> namespaces = reflector.M_AssemblyModel.m_Namespaces.ToList();
            List<TypeMetadata> types = namespaces[0].m_Types.ToList();
 
            List<PropertyMetadata> props;

            props = types[0].m_Properties.ToList();
            Assert.AreEqual(props.Count, 2);

            props = types[1].m_Properties.ToList();
            Assert.AreEqual(props.Count, 2);

            props = types[2].m_Properties.ToList();
            Assert.AreEqual(props.Count, 2);

        }


    }
}
