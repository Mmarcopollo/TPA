using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public static class ReflectionExtensions
    {
        public static string GetDescription(this AssemblyMetadata _assembly)
        {
            return _assembly.m_Name;
        }

        public static string GetDescription(this NamespaceMetadata _namespace)
        {
            return _namespace.m_NamespaceName;
        }

        public static string GetDescription(this TypeMetadata _type)
        {
            StringBuilder output = new StringBuilder();
            foreach (var attribute in _type.m_Attributes)
            {
                output.Append($"[{attribute.ToString()}] ");
            }

            output.Append(_type.m_typeName);

            if (_type.m_ImplementedInterfaces.Count<TypeMetadata>() > 0 || _type.m_BaseType != null)
            {
                output.Append(": ");
                if (_type.m_BaseType != null)
                {
                    output.Append($"{_type.m_BaseType.m_typeName}, ");
                }
                foreach (TypeMetadata impInterface in _type.m_ImplementedInterfaces)
                    output.Append($"{impInterface.m_typeName}, ");
                output.Remove(output.Length - 2, 2);
            }
            return output.ToString();
        }    

        public static string GetDescription(this MethodMetadata _method)
        {
            StringBuilder output = new StringBuilder();
            foreach (var attribute in _method.m_GenericArguments)
            {
                output.Append($"[{attribute.m_typeName}] ");
            }

            foreach (string modifier in _method.m_Modifiers)
                output.Append(modifier + " ");

            output.Append(_method.ReturnType.Name);
            output.Append($" {_method.Name}");

            output.Append(" (");
            if (_method.Parameters.Count > 0)
            {
                foreach (VarMetadata parameter in _method.Parameters)
                    output.Append(parameter.Type.Name + " " + parameter.Name + ", ");
                output.Remove(output.Length - 2, 2);
            }
            output.Append(")");

            return output.ToString();
        }

        public static string GetDescription(this IReflectionElement item)
        {
            if (item.GetType() == typeof(AssemblyMetadata))
            {
                return (item as AssemblyMetadata).GetDescription();
            }
            else if (item.GetType() == typeof(NamespaceMetadata))
            {
                return (item as NamespaceMetadata).GetDescription();
            }
            else if (item.GetType() == typeof(TypeMetadata))
            {
                return (item as TypeMetadata).GetDescription();
            }
            else if (item.GetType() == typeof(MethodMetadata))
            {
                return (item as MethodMetadata).GetDescription();
            }
            else throw new NotSupportedException("Extension method does not support external implementations of ReflectionElement");
        }

        public static IEnumerable<IReflectionElement> GetChildren(this IReflectionElement item)
        {
            if (item.GetType() == typeof(AssemblyMetadata))
            {
                var x = (AssemblyMetadata)item;
                return x.m_Namespaces;
            }
            else if (item.GetType() == typeof(NamespaceMetadata))
            {
                var x = (NamespaceMetadata)item;
                return x.m_Types;
            }
            else if (item.GetType() == typeof(TypeMetadata))
            {
                var x = (TypeMetadata)item;
                List<IReflectionElement> children = new List<IReflectionElement>();
                children.AddRange(x.Fields);
                children.AddRange(x.Properties);
                children.AddRange(x.Methods);
                children.AddRange(x.Attributes);
                children.AddRange(x.NestedTypes);
                children.AddRange(x.ImplementedInterfaces);
                children.AddRange(x.GenericArguments);
                if (x.BaseType != null)
                {
                    children.Add(x.BaseType);
                }
                return children;
            }
            else if (item.GetType() == typeof(MethodMetadata))
            {
                var x = (MethodMetadata)item;
                List<IReflectionElement> children = new List<IReflectionElement>();
                if (x.ReturnType.Name != "Void")
                    children.Add(x.ReturnType);
                children.AddRange(x.Parameters);
                children.AddRange(x.Attributes);
                return children;
            }
            return null;
        }
    }
}
