using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace ViewConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            TreeViewModel viewModel = new TreeViewModel();
            Console.WriteLine("Welcome in reflection Tree View program.");
            bool isSuccessfullyRead = true;

            do
            {
                if (!isSuccessfullyRead) Console.WriteLine("You typed wrong path. Try again.");
                Console.Write("Write the path to file you want load:");
                string path = Console.ReadLine();
                viewModel.PathVariable = path;
                isSuccessfullyRead = viewModel.LoadDLL();
            }
            while (!isSuccessfullyRead);

        }
    }
}

