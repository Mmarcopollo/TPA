using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using ViewModel;
using ViewModel.Treeview;

namespace ViewConsole.Convert
{
    [ValueConversion(typeof(TreeViewNode), typeof(Brush))]
    public class ToColorConverter : IValueConverter
    {
        public static ToColorConverter Instance = new ToColorConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type type = value.GetType();
            return type == typeof(AssemblyTreeView) ? ConsoleColor.White :
                type == typeof(MethodTreeView) ? ConsoleColor.Yellow :
                type == typeof(NamespacesTreeView) ? ConsoleColor.Red :
                type == typeof(ParameterTreeView) ? ConsoleColor.Magenta :
                type == typeof(PropertyTreeView) ? ConsoleColor.Green :
                type == typeof(TypeTreeView) ? ConsoleColor.DarkCyan :
                ConsoleColor.Gray;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
