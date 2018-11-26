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
    public class ColorConverter : IValueConverter
    {
        public static ColorConverter Instance = new ColorConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type type = value.GetType();
            return type == typeof(AssemblyTreeView) ? ConsoleColor.Cyan :
                type == typeof(MethodTreeView) ? ConsoleColor.DarkMagenta :
                type == typeof(NamespacesTreeView) ? ConsoleColor.Red :
                type == typeof(ParameterTreeView) ? ConsoleColor.Yellow :
                type == typeof(PropertyTreeView) ? ConsoleColor.Green :
                type == typeof(TypeTreeView) ? ConsoleColor.Gray :
                ConsoleColor.White;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
