using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace ViewConsole
{
    [Export(typeof(IBrowseFile))]
    public class BrowseFile : IBrowseFile
    {
        public string Browse()
        {
            Console.Write("Write the path to file:");
            return Console.ReadLine();
            //return "..\\..\\..\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
        }
    }
}
