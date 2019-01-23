using DataBaseLog;
using DataBaseLog.DTO;
using Log;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLoger
{
    [Export(typeof(ILogger))]
    public class DBLogger : ILogger
    {
        public void Log(string message)
        {
            using (DatabaseContextLog context = new DatabaseContextLog())
            {
                context.Logs.Add(new LogDTO()
                {
                    Message = message,
                    Time = DateTime.Now,
                });
                context.SaveChanges();
            }
        }
    }
}
