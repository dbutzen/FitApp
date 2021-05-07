using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCT.Utilities.Reporting.Models
{
    public class UserReport
    {
        public string Name { get; set; }
        public int CalorieGoal { get; set; }
        public int ProteinGoal { get; set; }

        public List<UserData> UserDataList { get; set; }
    }
}
