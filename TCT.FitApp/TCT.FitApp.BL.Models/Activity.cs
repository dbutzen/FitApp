using System;
using System.Collections.Generic;
using System.Text;

namespace TCT.FitApp.BL.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int EasyCaloriesPerHour { get; set; }
        public int MediumCaloriesPerHour { get; set; }
        public int HardCaloriesPerHour { get; set; }

    }
}
