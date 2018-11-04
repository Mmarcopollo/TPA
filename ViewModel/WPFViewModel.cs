using Microsoft.Win32;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ViewModel
{
    public class WPFViewModel : ViewModelBase
    {

        public WPFViewModel()
        {
            HierarchicalAreas = new ObservableCollection<TreeViewNode>();
            LoadDllCmd = new RelayCommand(pars => LoadDLL());
            BrowseCmd = new RelayCommand(pars => Browse());

        }

        public ObservableCollection<TreeViewNode> HierarchicalAreas { get; set; }
        public string PathVariable { get; set; }
        public Visibility ChangeControlVisibility { get; set; } = Visibility.Hidden;
        public ICommand BrowseCmd { get; }
        public ICommand LoadDllCmd { get; set; }
        private Reflector Reflector { get; set; }

        private void LoadDLL()
        {
            HierarchicalAreas.Clear();
            if (PathVariable.Substring(PathVariable.Length - 4) == ".dll")
                Reflector = new Reflector(PathVariable);
                TreeViewLoaded();
        }
        private void TreeViewLoaded()
        {
            TreeViewNode rootItem = new TreeViewNode { Element = Reflector.M_AssemblyModel };
            HierarchicalAreas.Add(rootItem);
        }

        private void Browse()
        {
            OpenFileDialog test = new OpenFileDialog()
            {
                Filter = "Dynamic Library File(*.dll)| *.dll"
            };
            test.ShowDialog();
            if (test.FileName.Length == 0)
                MessageBox.Show("No files selected");
            else
            {
                PathVariable = test.FileName;
                ChangeControlVisibility = Visibility.Visible;
                RaisePropertyChanged("ChangeControlVisibility");
                RaisePropertyChanged("PathVariable");
            }
        }
    }
}
