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

            DirectoryCatalog logger = new DirectoryCatalog("..\\..\\..\\DatabaseLogger\\bin\\Debug");
            DirectoryCatalog serialization = new DirectoryCatalog("..\\..\\..\\Database\\bin\\Debug");
            DirectoryCatalog browser = new DirectoryCatalog("..\\..\\..\\ViewWPF\\bin\\debug", "*.exe");

            _aggCatalog.Catalogs.Add(logger);
            _aggCatalog.Catalogs.Add(serialization);
            _aggCatalog.Catalogs.Add(browser);

            _container = new CompositionContainer(_aggCatalog);
            _container.ComposeParts(obj);

        }
    }
}
