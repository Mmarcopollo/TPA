using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewConsole
{
    public class Program
    {/*
        public static Metadata Element { get; set; }
        public static String FullName { get; set; }
        public List<TreeViewNode> Children { get; set; }
*/
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("Welcome in out program.");
            Console.Write("Write the path to file you want load:");

            string path = Console.ReadLine();

            Reflector reflector = new Reflector(path);

            Element = reflector.M_AssemblyModel;
            */

        }
        /*
        public void BuildMyself()
        {
            foreach (NamespaceMetadata namespaceMetadata in Element.GetAllNamespaces().OrEmptyIfNull())
            {
                Children.Add(new TreeViewNode { Element = namespaceMetadata, FullName = namespaceMetadata.Name + ":namespace" });
            }
            foreach (TypeMetadata typeMetadata in Element.GetAllTypes().OrEmptyIfNull())
            {
                Children.Add(new TreeViewNode { Element = typeMetadata, FullName = typeMetadata.Name + ":type" });
            }
            foreach (PropertyMetadata propertyMetadata in Element.GetAllProperties().OrEmptyIfNull())
            {
                Children.Add(new TreeViewNode { Element = propertyMetadata, FullName = propertyMetadata.Name + ":property" });
            }
            foreach (MethodMetadata methodMetadata in Element.GetAllMethods().OrEmptyIfNull())
            {
                Children.Add(new TreeViewNode { Element = methodMetadata, FullName = methodMetadata.Name + ":method" });
            }
            foreach (ParameterMetadata parameterMetadata in Element.GetAllParameters().OrEmptyIfNull())
            {
                Children.Add(new TreeViewNode { Element = parameterMetadata, FullName = parameterMetadata.Name + ":parameter" });
            }
        }
        */
    }
}

