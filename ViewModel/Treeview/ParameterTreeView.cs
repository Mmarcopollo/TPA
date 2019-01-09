using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Treeview
{
    public class ParameterTreeView : TreeViewNode
    {

        private ParameterMetadata _parameter;

        public ParameterTreeView(ParameterMetadata parameter)
        {
            _parameter = parameter;
            Name = parameter.Name;
            TypeOfMetadata = "parameter";
        }

        public override void BuildMyself(ObservableCollection<TreeViewNode> children)
        {
            if (_parameter != null)
            {
                if (TypeMetadata.TypeDictionary.ContainsKey(_parameter.UsedTypeMetadata.TypeName))
                    children.Add(new TypeTreeView(TypeMetadata.TypeDictionary[_parameter.UsedTypeMetadata.TypeName]));
                else
                    children.Add(new TypeTreeView((TypeMetadata)_parameter.UsedTypeMetadata));
            }
        }
    }
}
