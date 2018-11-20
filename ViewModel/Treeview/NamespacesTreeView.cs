using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Treeview
{
    public class NamespacesTreeView : TreeViewNode
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        private NamespaceMetadata _namespaceMeta;

        public NamespacesTreeView(NamespaceMetadata namespaceMeta)
        {
            _namespaceMeta = namespaceMeta;
            Name = namespaceMeta.Name;
            TypeOfMetadata = "namespace";
            log.Info("Namespace tree node was created.");
        }

        public override void BuildMyself(ObservableCollection<TreeViewNode> children)
        {
            if (_namespaceMeta.m_Types != null)
                foreach (var type in _namespaceMeta.m_Types)
                {
                    children.Add(new TypeTreeView(TypeMetadata.TypeDictionary[type.m_typeName]));
                }
            log.Info("Namespace tree node has expanded.");
        }
    }
}
