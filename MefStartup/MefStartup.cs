using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MefStartup
{
    public class MefStartup
    {
        private static MefStartup _instance = new MefStartup();

        public static MefStartup Instance
        {
            get { return _instance; }
        }

        private List<DirectoryCatalog> _extensionCatalogs = new List<DirectoryCatalog>();
        private AggregateCatalog _catalog;
        public CompositionContainer _container;

        public void AddCatalog(DirectoryCatalog obj)
        {
            _extensionCatalogs.Add(obj);
        }

        public void CreateCompositionContainer()
        {
            _catalog = new AggregateCatalog(_extensionCatalogs);
            _container = new CompositionContainer(_catalog);
        }

        public void ComposeParts(object obj)
        {
            _container.ComposeParts(obj);
        }


        //public static void Compose(object obj)
        //{

        //    CompositionContainer _container;
        //    AggregateCatalog _aggCatalog = new AggregateCatalog();

        //    _aggCatalog = new AggregateCatalog();

        //    DirectoryCatalog logger = new DirectoryCatalog("..\\..\\..\\FileLogger\\bin\\Debug");
        //    DirectoryCatalog serialization = new DirectoryCatalog("..\\..\\..\\Database\\bin\\Debug");
        //    DirectoryCatalog browser = new DirectoryCatalog("..\\..\\..\\ViewWPF\\bin\\debug", "*.exe");

        //    _aggCatalog.Catalogs.Add(logger);
        //    _aggCatalog.Catalogs.Add(serialization);
        //    _aggCatalog.Catalogs.Add(browser);

        //    _container = new CompositionContainer(_aggCatalog);
        //    _container.ComposeParts(obj);

        //}
    }
}
