using Log;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DTO
{
    [Table("Log")]
    public class DBLog
    {
        [Key]
        public int ID { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }

        public DBLog() { }

        public DBLog(string message, DateTime dateTime)
        {
            Message = message;
            Time = dateTime;
        }
    }
}
