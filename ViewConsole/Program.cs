using log4net.Config;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewConsole.Convert;
using ViewModel;

namespace ViewConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            TreeViewModel viewModel = new TreeViewModel(new BrowseFile());
            Console.WriteLine("Welcome in reflection Tree View program.");

            viewModel.ExecuteBrowseFile();
            viewModel.LoadDLL();
            

            Console.Clear();
            foreach (TreeViewNode node in viewModel.HierarchicalAreas) DisplayTree(node, 0);
            ConsoleKey control = Console.ReadKey().Key;

            while (control != ConsoleKey.Escape)
            {
                if (control == ConsoleKey.RightArrow)
                {
                    Console.Clear();
                    foreach (TreeViewNode node in viewModel.HierarchicalAreas)
                    {
                        ExpandTree(node);
                        DisplayTree(node, 0);
                    }
                }
                else if (control == ConsoleKey.LeftArrow)
                {
                    Console.Clear();
                    foreach (TreeViewNode node in viewModel.HierarchicalAreas)
                    {
                        ConvolveTree(node);
                        DisplayTree(node, 0);
                    }
                }
                else if (control == ConsoleKey.S)
                {
                    Console.Clear();
                    viewModel.Serialize();
                }
                else if (control == ConsoleKey.D)
                {
                    Console.Clear();
                    viewModel.Deserialize();
                    Console.Clear();
                    foreach (TreeViewNode node in viewModel.HierarchicalAreas)
                    {
                        ConvolveTree(node);
                        DisplayTree(node, 0);
                    }
                }
                control = Console.ReadKey().Key;
            }
        }

        private static void DisplayTree(TreeViewNode node, int level)
        {
            if(node != null)
            { 
                for (int i = 0; i < level; i++) Console.Write("  ");
                String textToDisplay = (String)ToStringConverter.Instance.Convert(node, null, null, null);
                Console.ForegroundColor = (ConsoleColor)ToColorConverter.Instance.Convert(node, null, null, null);
                Console.Write(textToDisplay);
                Console.ResetColor();
                Console.WriteLine(" " + node.Name);
                foreach (TreeViewNode childNode in node.Children)
                {
                    DisplayTree(childNode, level + 1);
                }
            }
        }

        private static void ExpandTree(TreeViewNode node)
        {
            if(node != null)
            {
                if (node.IsExpanded)
                {
                    foreach (TreeViewNode childNode in node.Children) ExpandTree(childNode);
                }
                else node.IsExpanded = true;
            }
        }

        private static void ConvolveTree(TreeViewNode node)
        {
            if(node != null)
            {
                node.IsExpanded = false;
                node.Children.Clear();
                node.WasBuilt = false;
            }
        }
    }
}

