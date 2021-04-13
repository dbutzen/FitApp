using System;
using System.Collections.Generic;

#nullable disable

namespace TCT.FitApp.PL
{
    public partial class TblActivity
    {
        public TblActivity()
        {
            TblDayActivities = new HashSet<TblDayActivity>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int EasyCaloriesPerHour { get; set; }
        public int MediumCaloriesPerHour { get; set; }
        public int HardCaloriesPerHour { get; set; }

        public virtual ICollection<TblDayActivity> TblDayActivities { get; set; }
    }
}
