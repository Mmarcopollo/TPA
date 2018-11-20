using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public abstract class TreeViewNode
    {
        public TreeViewNode()
        {
            Children = new ObservableCollection<TreeViewNode>() { null };
            this.WasBuilt = false;
        }

        public string Name { get; set; }
        public string TypeOfMetadata { get; set; }
        public ObservableCollection<TreeViewNode> Children { get; set; }
        public bool IsExpanded
        {
            get { return m_IsExpanded; }
            set
            {
                m_IsExpanded = value;
                if (WasBuilt)
                    return;
                Children.Clear();
                BuildMyself(Children);
                WasBuilt = true;
            }
        }

        public bool WasBuilt {private get => m_WasBuilt; set => m_WasBuilt = value; }

        private bool m_WasBuilt;
        private bool m_IsExpanded;
        public abstract void BuildMyself(ObservableCollection<TreeViewNode> children);
    }
}
