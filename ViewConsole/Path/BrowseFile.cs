using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace ViewConsole
{
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
