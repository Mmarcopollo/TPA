using BasicData;
using MEF;
using System;
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
        [Import(typeof(ISerializer))]
        public ISerializer Serialization
        {
            get; set;
        }

        public void SerializeAssembly(string path)
        {
            BaseAssemblyMetadata dataToSerialize = M_AssemblyModel;
            Serialization.Write(dataToSerialize, path);
        }

        public void DeserializeAssembly(string path)
        {
            BaseAssemblyMetadata deserializedData = Serialization.Read(path);
            M_AssemblyModel = new AssemblyMetadata(deserializedData);
        }

        public Reflector(string assemblyFile)
        {
            if (string.IsNullOrEmpty(assemblyFile))
                throw new System.ArgumentNullException();
            Assembly assembly = Assembly.ReflectionOnlyLoadFrom(assemblyFile);
            M_AssemblyModel = new AssemblyMetadata(assembly);
            MefStartup.Instance.ComposeParts(this);
        }
        public Reflector(Assembly assembly)
        {
            M_AssemblyModel = new AssemblyMetadata(assembly);
            MefStartup.Instance.ComposeParts(this);
        }
        public Reflector()
        {
            MefStartup.Instance.ComposeParts(this);
        }

        public Reflector(AssemblyMetadata assembly)
        {
            M_AssemblyModel = assembly;
            TypeMetadata.TypeDictionary.Clear();
            M_AssemblyModel.Namespaces.ToList().ForEach(ns => ns.Types.ToList().ForEach(t => TypeMetadata.TypeDictionary.Add(t.TypeName, (TypeMetadata)t)));
        }
        [DataMember]
        public AssemblyMetadata M_AssemblyModel { get; set; }
    }
}
