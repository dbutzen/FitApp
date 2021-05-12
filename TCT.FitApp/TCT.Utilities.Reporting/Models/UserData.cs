using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCT.Utilities.Reporting.Models
{
    public class UserData
    {
        public DateTime Date { get; set; }
        public int Activities { get; set; }
        public int CaloriesConsumed { get; set; }
        public double CaloriesBurned { get; set; }
        public int ProteinConsumed { get; set; }
        public string Succeeded { get; set; }
    }
}
