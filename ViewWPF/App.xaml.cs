using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Windows;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using System.Configuration;
using MEF;

namespace ViewWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NameValueCollection plugins = (NameValueCollection)ConfigurationManager.GetSection("dirpaths");
            string[] pluginsCatalogs = plugins.AllKeys;
            foreach (string pluginsCatalog in pluginsCatalogs)
            {
                if (Directory.Exists(pluginsCatalog))
                    MefStartup.Instance.AddCatalog(new DirectoryCatalog(pluginsCatalog));
            }
            MefStartup.Instance.CreateCompositionContainer();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            MefStartup.Instance.Dispose();
        }
    }
}

