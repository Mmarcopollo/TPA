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
            Name = type.TypeName;
            TypeOfMetadata = "type";
        }

        public override void BuildMyself(ObservableCollection<TreeViewNode> children)
        {
            if (_type.Fields != null)
                foreach (var field in _type.Fields)
                {
                    children.Add(new FieldTreeView(field));
                }
            if (_type.Properties != null)
                foreach (PropertyMetadata property in _type.Properties)
                {
                    children.Add(new PropertyTreeView(property));
                }

            if (_type.Methods != null)
                foreach (MethodMetadata method in _type.Methods)
                {
                    children.Add(new MethodTreeView(method));
                }

        }
    }
}
