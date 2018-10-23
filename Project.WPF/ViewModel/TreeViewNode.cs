using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.WPF.ViewModel
{
    class TreeViewNode
    {
        public TreeViewNode()
        {
            Children = new ObservableCollection<TreeViewNode>() { null };
            this.m_WasBuilt = false;
        }
        public string Name { get; set; }
        public ObservableCollection<TreeViewNode> Children { get; set; }
        public bool IsExpanded
        {
            get { return m_IsExpanded; }
            set
            {
                m_IsExpanded = value;
                if (m_WasBuilt)
                    return;
                Children.Clear();
                BuildMyself();
                m_WasBuilt = true;
            }
        }

        private bool m_WasBuilt;
        private bool m_IsExpanded;
        private void BuildMyself()
        {

        }
    }
}
