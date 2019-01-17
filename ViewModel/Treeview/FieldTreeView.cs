using Model.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Treeview
{
    public class FieldTreeView : TreeViewNode
    {
        private  FieldMetadata _field;

        public FieldTreeView(FieldMetadata field) 
        {
            _field = field;
            Name = field.FieldName;
            TypeOfMetadata = "field";
        }

        public override void BuildMyself(ObservableCollection<TreeViewNode> children)
        {

            if (_field.FieldType != null) // Type
            {
                children.Add(new TypeTreeView(_field.FieldType));
            }
        }
    }
}
