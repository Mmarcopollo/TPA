using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Treeview
{
    public class PropertyTreeView : TreeViewNode
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        private PropertyMetadata _property;

        public PropertyTreeView(PropertyMetadata property)
        {
            _property = property;
            Name = property.Name;
            TypeOfMetadata = "property";
            log.Info("Property tree node was created.");
        }

        public override void BuildMyself(ObservableCollection<TreeViewNode> children)
        {
            if (_property != null)
            {
                if (TypeMetadata.TypeDictionary.ContainsKey(_property.m_TypeMetadata.m_typeName))
                    children.Add(new TypeTreeView(TypeMetadata.TypeDictionary[_property.m_TypeMetadata.m_typeName]));
                else
                    children.Add(new TypeTreeView(_property.m_TypeMetadata));
            }
            log.Info("Property tree node has expanded.");
        }
    }
}
