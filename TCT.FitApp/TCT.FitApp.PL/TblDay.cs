using System;
using System.Collections.Generic;

#nullable disable

namespace TCT.FitApp.PL
{
    public partial class TblDay
    {
        public TblDay()
        {
            TblDayActivities = new HashSet<TblDayActivity>();
            TblDayItems = new HashSet<TblDayItem>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public bool Succeeded { get; set; }

        public virtual TblUser User { get; set; }
        public virtual ICollection<TblDayActivity> TblDayActivities { get; set; }
        public virtual ICollection<TblDayItem> TblDayItems { get; set; }
    }
}
