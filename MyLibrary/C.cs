using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class C
    {
        public A classA;

        public int Z { get; set; }

        public float  MethodInC(B classB)
        {
            return classB.Y;
        }
    }
}
