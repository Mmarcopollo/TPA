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
    }
}

