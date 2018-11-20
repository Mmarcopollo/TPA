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
        }

        public override void BuildMyself(ObservableCollection<TreeViewNode> children)
        {
            var namespacesList = _assembly.Namespaces.ToList();
            if (namespacesList != null)
            {
                foreach (var name in namespacesList)
                {
                    children.Add(new NamespacesTreeView(name));
                }
            }
        }
    }
}
