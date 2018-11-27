using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel;

namespace ViewWPF
{
    public class BrowseFile : IBrowseFile
    {
        public string Browse()
        {
            OpenFileDialog test = new OpenFileDialog()
            {
                Filter = "Dynamic Library File(*.dll)| *.dll|Executable(*.exe)| *.exe| *.xml"
            };
            test.ShowDialog();
            return test.FileName;
        }
    }
}
