﻿using Model;
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
                if (TypeMetadata.TypeDictionary.ContainsKey(_parameter.m_TypeMetadata.m_typeName))
                    children.Add(new TypeTreeView(TypeMetadata.TypeDictionary[_parameter.m_TypeMetadata.m_typeName]));
                else
                    children.Add(new TypeTreeView(_parameter.m_TypeMetadata));
            }
        }
    }
}
