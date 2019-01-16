using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Treeview
{
    public class AttributeTreeView : TreeViewNode
    {
        private TypeMetadata _attribute;

        public AttributeTreeView(TypeMetadata attribute)
        {
            _attribute = attribute;
            Name = attribute.TypeName;
            TypeOfMetadata = "attribute";
        }


        public override void BuildMyself(ObservableCollection<TreeViewNode> children)
        {
           
        }
    }
}
