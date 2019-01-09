using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Treeview
{
    public class MethodTreeView : TreeViewNode
    {
      

        private MethodMetadata _method;
        public MethodTreeView(MethodMetadata method)
        {
            _method = method;
            Name = method.Name;
            TypeOfMetadata = "method";
        }

        public override void BuildMyself(ObservableCollection<TreeViewNode> children)
        {
            if (_method.GenericArguments != null)
            {
                foreach (TypeMetadata argument in _method.GenericArguments)
                {
                    children.Add(new TypeTreeView(argument));
                }
            }
            if (_method.Parameters != null)
            {
                foreach (ParameterMetadata parameter in _method.Parameters)
                {
                    children.Add(new ParameterTreeView(parameter));
                }
            }

        }
    }
}
