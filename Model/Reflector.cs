﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class Reflector
    {

        public Reflector(string assemblyFile)
        {
            if (string.IsNullOrEmpty(assemblyFile))
                throw new System.ArgumentNullException();
            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            M_AssemblyModel = new AssemblyMetadata(assembly);
            Compose(M_AssemblyModel);
        }
        public Reflector(Assembly assembly)
        {
            M_AssemblyModel = new AssemblyMetadata(assembly);
            Compose(M_AssemblyModel);
        }

        public Reflector() { }
        public Reflector(AssemblyMetadata assembly)
        {
            M_AssemblyModel = assembly;
            TypeMetadata.TypeDictionary.Clear();
            M_AssemblyModel.m_Namespaces.ToList().ForEach(ns => ns.m_Types.ToList().ForEach(t => TypeMetadata.TypeDictionary.Add(t.Name, t)));
        }
        [DataMember]
        public AssemblyMetadata M_AssemblyModel { get; set; }


        private CompositionContainer _container;
        private AggregateCatalog _aggCatalog = new AggregateCatalog();

        public void Compose(object obj)
        {

            _aggCatalog = new AggregateCatalog();
            DirectoryCatalog serialize = new DirectoryCatalog("..\\..\\..\\Serialization\\bin\\Debug");
            _aggCatalog.Catalogs.Add(serialize);

            _container = new CompositionContainer(_aggCatalog);
            _container.ComposeParts(obj);


        }
    }
}
