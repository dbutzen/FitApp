using System;
using System.Collections.Generic;

#nullable disable

namespace TCT.FitApp.PL
{
    public partial class tblDayItem
    {
        public Guid Id { get; set; }
        public Guid DayId { get; set; }
        public Guid ItemId { get; set; }
        public int Servings { get; set; }

        public virtual tblDay Day { get; set; }
        public virtual tblItem Item { get; set; }
    }
}
