using log4net;
using log4net.Config;
using Microsoft.Win32;
using Model;
using Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewModel.Treeview;

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
            BrowseCmd = new RelayCommand(pars => ExecuteBrowseFile());

        }
        public void ExecuteBrowseFile()
        {
            if (FilePathProvider != null)
            {
                PathVariable = FilePathProvider.Browse();
            }
        }

        public TreeViewModel(IBrowseFile pathProvider)
        {
            FilePathProvider = pathProvider;
            XmlConfigurator.Configure();
            HierarchicalAreas = new ObservableCollection<TreeViewNode>();
            LoadDllCmd = new RelayCommand(pars => LoadDLL());
            BrowseCmd = new RelayCommand(pars => ExecuteBrowseFile());
            SerializeToXmlCommand = new RelayCommand(pars => SerializeToXml());
            DeserializeFromXmlCommand = new RelayCommand(pars => DeserializeFromXml());
            xmlSerializer = new XMLSerialization();
        }

        public ObservableCollection<TreeViewNode> HierarchicalAreas { get; set; }

        private string _pathVariable;
        public string PathVariable
        {
            get
            {
                return _pathVariable;
            }
            set
            {
                _pathVariable = value;
                RaisePropertyChanged();
            }
        }
        public ICommand BrowseCmd { get; }
        public ICommand LoadDllCmd { get; set; }

        public ICommand SerializeToXmlCommand { get; }
        public ICommand DeserializeFromXmlCommand { get; }
        public static Reflector Reflector { get; set; }
        public XMLSerialization xmlSerializer;

        public IBrowseFile FilePathProvider
        {
            get;
        }

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
            TreeViewNode rootItem = new AssemblyTreeView(Reflector.M_AssemblyModel);
            HierarchicalAreas.Add(rootItem);
            log.Info("TreeView is loaded");
        }

        public void SerializeToXml()
        {
            string pathToSaveSerializedFile = FilePathProvider.Browse();

            xmlSerializer.Serialize(Reflector.M_AssemblyModel, pathToSaveSerializedFile);
        }

        public void DeserializeFromXml()
        {

            string pathToSerializedFile = FilePathProvider.Browse();

            if (pathToSerializedFile != null)
            {
                AssemblyMetadata deserializedAssemblyReader = xmlSerializer.Deserialize(pathToSerializedFile);
                Reflector = new Reflector(deserializedAssemblyReader);

                HierarchicalAreas.Clear();
                HierarchicalAreas.Add(new AssemblyTreeView(Reflector.M_AssemblyModel));


            }
        }
    }
}
