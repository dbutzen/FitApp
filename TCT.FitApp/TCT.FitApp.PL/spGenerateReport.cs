using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCT.FitApp.PL
{
    public class spGenerateReport
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }

        public int ActivityCount { get; set; }
        public int CalorieGoal { get; set; }
        public int CaloriesConsumed { get; set; }
        public double CaloriesBurned { get; set; }
        public int ProteinGoal { get; set; }
        public int ProteinConsumed { get; set; }
        public bool Succeeded { get; set; }
    }
}
