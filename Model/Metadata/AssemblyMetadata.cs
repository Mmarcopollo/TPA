﻿using Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AssemblyMetadata
    {
        public AssemblyMetadata(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            Namespaces = from Type _type in assembly.GetTypes()
                           where _type.GetVisible()
                           group _type by _type.GetNamespace() into _group
                           orderby _group.Key
                           select new NamespaceMetadata(_group.Key, _group);
        }

        public AssemblyMetadata(AssemblyMetadataDTO assemblyMetadataDTO)
        {
            m_Name = assemblyMetadataDTO.m_Name;
            if(assemblyMetadataDTO.m_Namespaces != null)
            {
                List<NamespaceMetadata> namespaces = new List<NamespaceMetadata>();
                foreach (NamespaceMetadataDTO DTO in assemblyMetadataDTO.m_Namespaces)
                {
                    NamespaceMetadata methodMetadata = new NamespaceMetadata(DTO);
                    namespaces.Add(methodMetadata);
                }
                m_Namespaces = namespaces;
            }
        }

        public string m_Name;
        public IEnumerable<NamespaceMetadata> m_Namespaces;
        
        public string Name { get => m_Name; set => m_Name = value; }
        public IEnumerable<NamespaceMetadata> Namespaces { get => m_Namespaces; set => m_Namespaces = value; }
    
    }
}
