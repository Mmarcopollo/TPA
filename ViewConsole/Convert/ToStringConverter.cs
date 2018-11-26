using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ViewModel;
using ViewModel.Treeview;

namespace ViewConsole.Convert
{
    [ValueConversion(typeof(TreeViewNode), typeof(string))]
    public class ToStringConverter : IValueConverter
    {
        public static ToStringConverter Instance = new ToStringConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type type = value.GetType();
            string typeString = type == typeof(AssemblyTreeView) ? "Assembly" :
                type == typeof(MethodTreeView) ? "Method" :
                type == typeof(NamespacesTreeView) ? "Namespace" :
                type == typeof(ParameterTreeView) ? "Parameter" :
                type == typeof(PropertyTreeView) ? "Property" :
                type == typeof(TypeTreeView) ? "Type" : "";
            return '[' + typeString + ']';

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
