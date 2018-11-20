using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Treeview
{
    public class AssemblyTreeView : TreeViewNode
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        private AssemblyMetadata _assembly;

        public AssemblyTreeView(AssemblyMetadata assembly)
        {
            _assembly = assembly;
            Name = assembly.Name;
            TypeOfMetadata = "assembly";
            log.Info("Assembly tree node was created.");
        }

        public override void BuildMyself(ObservableCollection<TreeViewNode> children)
        {
            List<NamespaceMetadata> namespacesList = _assembly.Namespaces.ToList();
            if (namespacesList != null)
            {
                foreach (NamespaceMetadata name in namespacesList)
                {
                    children.Add(new NamespacesTreeView(name));
                }
            }
            log.Info("Assembly tree node has expanded.");
        }
    }
}
