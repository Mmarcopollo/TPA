using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                //viewModel.PathVariable = path;
                viewModel.PathVariable = "C:\\Users\\Ola\\Desktop\\TPA\\TPA\\MyLibrary\\bin\\Debug\\MyLibrary.dll";
                isSuccessfullyRead = viewModel.LoadDLL();
            }
            while (!isSuccessfullyRead);

            foreach(TreeViewNode node in viewModel.HierarchicalAreas)
            {
                DisplayTree(node, 0);
            }

        }

        private static void DisplayTree(TreeViewNode node, int level)
        {
            if(node != null)
            { 
                for (int i = 0; i < level; i++) Console.WriteLine("    ");
                Console.WriteLine(node.FullName + "\n");
                foreach (TreeViewNode childNode in node.Children)
                {
                    DisplayTree(childNode, level + 1);
                }
            }
        }
    }
}

