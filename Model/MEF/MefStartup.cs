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

            DirectoryCatalog exe = new DirectoryCatalog("..\\..\\..\\DllToCompose", "*.exe");
            DirectoryCatalog dll = new DirectoryCatalog("..\\..\\..\\DllToCompose");

            _aggCatalog.Catalogs.Add(exe);
            _aggCatalog.Catalogs.Add(dll);

            _container = new CompositionContainer(_aggCatalog);
            _container.ComposeParts(obj);

        }
    }
}
