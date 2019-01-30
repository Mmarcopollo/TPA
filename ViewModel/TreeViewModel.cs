using Log;
using MEF;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Input;
using ViewModel.Treeview;

namespace ViewModel
{
    public class TreeViewModel : ViewModelBase
    {

        public TreeViewModel()
        {
            MefStartup.Instance.ComposeParts(this);
            HierarchicalAreas = new ObservableCollection<TreeViewNode>();
            LoadDllCmd = new RelayCommand(pars => LoadDLL());
            BrowseCmd = new RelayCommand(pars => ExecuteBrowseFile());
            SerializeCommand = new RelayCommand(pars => Serialize());
            DeserializeCommand = new RelayCommand(pars => Deserialize());

        }
        public void ExecuteBrowseFile()
        {
            if (FilePathProvider != null)
            {
                PathVariable = FilePathProvider.Browse();
            }
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
        public ICommand SerializeCommand { get; }
        public ICommand DeserializeCommand { get; }
        public Reflector Reflector { get; set; }

        #region MEF
        [Import(typeof(IBrowseFile))]
        public IBrowseFile FilePathProvider
        {
            get; set;
        }
        [Import(typeof(ILogger))]
        public ILogger Logger
        {
            get; set;
        }

        #endregion

        public bool LoadDLL()
        {
            Logger.Log("Loading DLL.");
            HierarchicalAreas.Clear();
            if (PathVariable.Length > 4 && (PathVariable.Substring(PathVariable.Length - 4) == ".dll" || PathVariable.Substring(PathVariable.Length - 4) == ".exe"))
            {
                Reflector = new Reflector(PathVariable);
                TreeViewLoaded();
                Logger.Log("File loaded to treeview.");
                return true;
            }
            else
            {
                Logger.Log("File failed when loading from path");
                return false;
            }
        }
        public void TreeViewLoaded()
        {
            TreeViewNode rootItem = new AssemblyTreeView(Reflector.M_AssemblyModel);
            HierarchicalAreas.Add(rootItem);
            Logger.Log("TreeView is loaded");
        }

        public void Serialize()
        {
            if(Reflector != null)
            {
                string pathToSaveSerializedFile = FilePathProvider.Browse();

                if( pathToSaveSerializedFile != "" ) Reflector.SerializeAssembly(pathToSaveSerializedFile);
            }
        }

        public void Deserialize()
        {

            string pathToSerializedFile = FilePathProvider.Browse();

            if (pathToSerializedFile != null)
            {
                Reflector = new Reflector();
                Reflector.DeserializeAssembly(pathToSerializedFile);

                HierarchicalAreas.Clear();
                TreeViewLoaded();
            }
        }


    }
}
