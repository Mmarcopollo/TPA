using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;

namespace Model.MEF
{
    public class MefStartup
    {

        public static void Compose(object obj)
        {
            CompositionContainer _container;
            AggregateCatalog _aggCatalog = new AggregateCatalog();

            _aggCatalog = new AggregateCatalog();
            DirectoryCatalog logger = new DirectoryCatalog("..\\..\\..\\FileLogger\\bin\\Debug");
            DirectoryCatalog serialize = new DirectoryCatalog("..\\..\\..\\Serialization\\bin\\Debug");
            DirectoryCatalog thisDirectory = new DirectoryCatalog(Directory.GetCurrentDirectory(), "*.exe");

            _aggCatalog.Catalogs.Add(logger);
            _aggCatalog.Catalogs.Add(serialize);
            _aggCatalog.Catalogs.Add(thisDirectory);

            _container = new CompositionContainer(_aggCatalog);
            _container.ComposeParts(obj);

        }
    }
}
