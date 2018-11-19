using log4net;
using log4net.Config;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;

namespace ViewWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TreeViewModel viewModel = new TreeViewModel(new BrowseFile());
     
        public MainWindow()
        {
            XmlConfigurator.Configure();
            this.Loaded += (s, e) => { this.DataContext = this.viewModel; };
            InitializeComponent();            
        }
    }
}
