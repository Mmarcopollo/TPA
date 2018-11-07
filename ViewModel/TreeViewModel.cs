using log4net;
using log4net.Config;
using Microsoft.Win32;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ViewModel
{
    public class TreeViewModel : ViewModelBase
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        public TreeViewModel()
        {
            XmlConfigurator.Configure();
            HierarchicalAreas = new ObservableCollection<TreeViewNode>();
            LoadDllCmd = new RelayCommand(pars => LoadDLL());
            BrowseCmd = new RelayCommand(pars => Browse());

        }

        public ObservableCollection<TreeViewNode> HierarchicalAreas { get; set; }
        public string PathVariable { get; set; }
        public Visibility ChangeControlVisibility { get; set; } = Visibility.Hidden;
        public ICommand BrowseCmd { get; }
        public ICommand LoadDllCmd { get; set; }
        public Reflector Reflector { get; set; }

        public bool LoadDLL()
        {
            log.Info("Loading DLL.");
            HierarchicalAreas.Clear();
            if (PathVariable.Length > 4 && (PathVariable.Substring(PathVariable.Length - 4) == ".dll" || PathVariable.Substring(PathVariable.Length - 4) == ".exe"))
            {
                Reflector = new Reflector(PathVariable);
                TreeViewLoaded();
                log.Info("File loaded to treeview.");
                return true;
            }
            else
            {
                log.Info("File failed when loading from path");
                return false;
            }
        }
        public void TreeViewLoaded()
        {
        TreeViewNode rootItem = new TreeViewNode { Element = Reflector.M_AssemblyModel, FullName = Reflector.M_AssemblyModel.Name + ":assembly" };
            HierarchicalAreas.Add(rootItem);
            log.Info("TreeView is loaded");
        }

        public void Browse()
        {
            OpenFileDialog test = new OpenFileDialog()
            {
                Filter = "Dynamic Library File(*.dll)| *.dll|Executable(*.exe)| *.exe"
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

                log.Info("The file to load was selected");
            }
        }
    }
}
