using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Treeview
{
    public class TypeTreeView : TreeViewNode
    {

        private TypeMetadata _type;
        public TypeTreeView(TypeMetadata type) 
        {
            _type = type;
            Name = type.Name;
            TypeOfMetadata = "type";
        }

        public override void BuildMyself(ObservableCollection<TreeViewNode> children)
        {
            if (_type.m_Properties != null)
                foreach (PropertyMetadata property in _type.m_Properties)
                {
                    children.Add(new PropertyTreeView(property));
                }

            if (_type.m_Methods != null)
                foreach (MethodMetadata method in _type.m_Methods)
                {
                    children.Add(new MethodTreeView(method));
                }
        }
    }
}
