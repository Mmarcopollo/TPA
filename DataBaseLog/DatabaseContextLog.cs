using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using DatabaseLog.DTO;

namespace DatabaseLog
{
    public class DatabaseContextLog : DbContext
    {
        public DatabaseContextLog() : base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Project;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework") { }

        public virtual DbSet<LogDTO> Logs { get; set; }
    }
}
