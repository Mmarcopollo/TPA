using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    internal static class ExtensionMethods
    {
        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }
    }
}
