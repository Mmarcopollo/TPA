using Log;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLogger
{
    [Export(typeof(ILogger))]
    public class Logger : ILogger
    {
        public void Log(string message)
        {

            string now = DateTime.Now.ToString();

            System.IO.StreamWriter file = new System.IO.StreamWriter("..\\..\\..\\Logs\\LogFile.log", true);
            file.WriteLine(now + message);

            file.Close();

        }
    }
}
