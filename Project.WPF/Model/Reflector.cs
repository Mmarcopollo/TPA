using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TPA.Reflection.Model;

namespace Project.WPF.Model
{
    class Reflector
    {
        public Reflector(string assemblyFile)
        {
            if (string.IsNullOrEmpty(assemblyFile))
                throw new System.ArgumentNullException();
            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            m_AssemblyModel = new AssemblyMetadata(assembly);
        }
        public Reflector(Assembly assembly)
        {
            m_AssemblyModel = new AssemblyMetadata(assembly);
        }
        internal AssemblyMetadata m_AssemblyModel { get; private set; }
    }
}
