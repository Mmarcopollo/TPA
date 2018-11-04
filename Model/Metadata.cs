using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class Metadata
    {
        public abstract String Name { get; set; }
        public abstract IEnumerable<NamespaceMetadata> GetAllNamespaces();
        public abstract IEnumerable<TypeMetadata> GetAllTypes();
        public abstract IEnumerable<PropertyMetadata> GetAllProperties();
        public abstract IEnumerable<MethodMetadata> GetAllMethods();
        public abstract IEnumerable<ParameterMetadata> GetAllParameters();
        //public bool IsExpandable()
        //{
        //    IEnumerable<NamespaceMetadata> n_temp = GetAllNamespaces();
        //    IEnumerable<TypeMetadata> t_temp = GetAllTypes();
        //    IEnumerable<PropertyMetadata> pr_temp = GetAllProperties();
        //    IEnumerable<MethodMetadata> m_temp = GetAllMethods();
        //    IEnumerable<ParameterMetadata> pa_temp = GetAllParameters();
        //    if (n_temp == null || n_temp.Count() == 0 &&
        //        t_temp == null || t_temp.Count() == 0 &&
        //        pr_temp == null || pr_temp.Count() == 0 &&
        //        m_temp == null || m_temp.Count() == 0 &&
        //        pa_temp == null || pa_temp.Count() == 0) return false;
        //    return true;
        //}
    }
}
