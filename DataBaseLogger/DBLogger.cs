using Database;
using Database.DTO;
using Log;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLogger
{
    [Export(typeof(ILogger))]
    public class DBLogger : ILogger
    {
        public void Log(string message)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.Log.Add(new DBLog()
                {
                    Message = message,
                    Time = DateTime.Now
                });
            }
        }
    }
}
