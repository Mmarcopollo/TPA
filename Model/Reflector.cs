using System;
using System.Collections.Generic;
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
        }
        public Reflector(Assembly assembly)
        {
            M_AssemblyModel = new AssemblyMetadata(assembly);
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
    }
}
