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

namespace ViewWPF.Convert
{
    [ValueConversion(typeof(TreeViewNode), typeof(Brush))]
    public class ToBrushConverter :IValueConverter
    {
        public static ToBrushConverter Instance = new ToBrushConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type type = value.GetType();
            return type == typeof(AssemblyTreeView) ? new SolidColorBrush(Colors.BlueViolet) :
                type == typeof(MethodTreeView) ? new SolidColorBrush(Colors.Salmon) :
                type == typeof(NamespacesTreeView) ? new SolidColorBrush(Colors.DarkBlue) :
                type == typeof(ParameterTreeView) ? new SolidColorBrush(Colors.SeaGreen) :
                type == typeof(PropertyTreeView) ? new SolidColorBrush(Colors.OrangeRed) :
                type == typeof(TypeTreeView) ? new SolidColorBrush(Colors.Fuchsia) :
                new SolidColorBrush(Colors.Black);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
