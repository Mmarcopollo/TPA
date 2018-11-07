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

            List<NamespaceMetadata> namespaces = reflector.M_AssemblyModel.Namespaces.ToList();
            Assert.AreEqual(namespaces.Count, 1);
            List<TypeMetadata> types = namespaces[0].m_Types.ToList();
            Assert.AreEqual(types.Count, 3);
            List<MethodMetadata> methods;
            List<PropertyMetadata> props;

            methods = types[0].m_Methods.ToList();
            props = types[0].m_Properties.ToList();
            Assert.AreEqual(methods.Count, 8);
            Assert.AreEqual(props.Count, 2);

            methods = types[1].m_Methods.ToList();
            props = types[1].m_Properties.ToList();
            Assert.AreEqual(methods.Count, 8);
            Assert.AreEqual(props.Count, 2);

            methods = types[2].m_Methods.ToList();
            props = types[2].m_Properties.ToList();
            Assert.AreEqual(methods.Count, 9);
            Assert.AreEqual(props.Count, 2);

        }


    }
}
