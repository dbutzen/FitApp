using System;
using System.Collections.Generic;

#nullable disable

namespace TCT.FitApp.PL
{
    public partial class tblDay
    {
        public tblDay()
        {
            TblDayActivities = new HashSet<tblDayActivity>();
            TblDayItems = new HashSet<tblDayItem>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public bool Succeeded { get; set; }

        public virtual tblUser User { get; set; }
        public virtual ICollection<tblDayActivity> TblDayActivities { get; set; }
        public virtual ICollection<tblDayItem> TblDayItems { get; set; }
    }
}
