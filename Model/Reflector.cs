using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
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
        public AssemblyMetadata M_AssemblyModel { get;  set; }
    }
}
