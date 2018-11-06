using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class TreeViewNode
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        public TreeViewNode()
        {
            Children = new ObservableCollection<TreeViewNode>() { null };
            this.m_WasBuilt = false;
        }
        public Metadata Element { get; set; }
        public String FullName { get; set; }
        public ObservableCollection<TreeViewNode> Children { get; set; }
        public bool IsExpanded
        {
            get { return m_IsExpanded; }
            set
            {
                m_IsExpanded = value;
                if (m_WasBuilt)
                    return;
                Children.Clear();
                BuildMyself();
                m_WasBuilt = true;
            }
        }

        private bool m_WasBuilt;
        private bool m_IsExpanded;
        private void BuildMyself()
        {
            foreach(NamespaceMetadata namespaceMetadata in Element.GetAllNamespaces().OrEmptyIfNull())
            {
                Children.Add(new TreeViewNode { Element = namespaceMetadata, FullName = namespaceMetadata.Name + ":namespace" });

            }
           foreach (TypeMetadata typeMetadata in Element.GetAllTypes().OrEmptyIfNull())
            {
                Children.Add(new TreeViewNode { Element = TypeMetadata.TypeDictionary[typeMetadata.m_typeName], FullName = typeMetadata.Name + ":type" });
            }
            foreach (PropertyMetadata propertyMetadata in Element.GetAllProperties().OrEmptyIfNull())
            {
                Children.Add(new TreeViewNode { Element = propertyMetadata, FullName = propertyMetadata.Name + ":property" });
            }
            foreach (MethodMetadata methodMetadata in Element.GetAllMethods().OrEmptyIfNull())
            {
                Children.Add(new TreeViewNode { Element = methodMetadata, FullName = methodMetadata.Name + ":method" });
            }
            foreach (ParameterMetadata parameterMetadata in Element.GetAllParameters().OrEmptyIfNull())
            {
                Children.Add(new TreeViewNode { Element = parameterMetadata, FullName = parameterMetadata.Name + ":parameter" });
            }
            log.Info("Method BuildMyself work properly.");
            
        }
    }
}
