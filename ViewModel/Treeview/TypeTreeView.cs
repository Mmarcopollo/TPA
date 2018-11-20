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
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        private TypeMetadata _type;
        public TypeTreeView(TypeMetadata type) 
        {
            _type = type;
            Name = type.Name;
            TypeOfMetadata = "type";
            log.Info("Type tree node was created.");
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
            log.Info("Type tree node has expanded.");
        }
    }
}
