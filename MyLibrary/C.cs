using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    class C
    {
        private int z;
        private A classA;

        public int Z { get => z; set => z = value; }
        internal A ClassA { get => classA; set => classA = value; }
    }
}
