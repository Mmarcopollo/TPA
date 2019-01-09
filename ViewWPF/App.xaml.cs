using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Windows;
using ViewModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using System.Configuration;
using Model.MEF;

namespace ViewWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private CompositionContainer _container;
        private AggregateCatalog _aggCatalog = new AggregateCatalog();

        public void Start(object sender, StartupEventArgs e)
        {
            TreeViewModel tv = new TreeViewModel();
            MainWindow window = new MainWindow
            {
                DataContext = tv
            };
            window.Show();
            Application.Current.MainWindow = window;

        }

        public void Compose(object obj)
        {

            _aggCatalog = new AggregateCatalog();
            DirectoryCatalog logger = new DirectoryCatalog("..\\..\\..\\Log\\bin\\Debug");
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

