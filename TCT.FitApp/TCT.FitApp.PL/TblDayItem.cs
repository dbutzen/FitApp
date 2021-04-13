using System;
using System.Collections.Generic;

#nullable disable

namespace TCT.FitApp.PL
{
    public partial class TblDayItem
    {
        public Guid Id { get; set; }
        public Guid DayId { get; set; }
        public Guid ItemId { get; set; }
        public int Servings { get; set; }

        public virtual TblDay Day { get; set; }
        public virtual TblItem Item { get; set; }
    }
}
