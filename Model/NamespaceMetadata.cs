﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class NamespaceMetadata
    {

        internal NamespaceMetadata(string name, IEnumerable<Type> types)
        {
            m_NamespaceName = name;
            m_Types = from type in types orderby type.Name select new TypeMetadata(type);
        }

        private string m_NamespaceName;
        private IEnumerable<TypeMetadata> m_Types;

    }
}
