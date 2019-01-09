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

        private AssemblyMetadata _assembly;

        public AssemblyTreeView(AssemblyMetadata assembly)
        {
            _assembly = assembly;
            Name = assembly.Name;
            TypeOfMetadata = "assembly";
        }

        public override void BuildMyself(ObservableCollection<TreeViewNode> children)
        {
            List<NamespaceMetadata> namespacesList = ((IEnumerable<NamespaceMetadata>)_assembly.Namespaces).ToList();
            if (namespacesList != null)
            {
                foreach (NamespaceMetadata name in namespacesList)
                {
                    children.Add(new NamespacesTreeView(name));
                }
            }
        }
    }
}
