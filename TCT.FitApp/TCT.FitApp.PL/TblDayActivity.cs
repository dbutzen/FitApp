using System;
using System.Collections.Generic;

#nullable disable

namespace TCT.FitApp.PL
{
    public partial class TblDayActivity
    {
        public Guid Id { get; set; }
        public Guid DayId { get; set; }
        public Guid ActivityId { get; set; }
        public int Duration { get; set; }
        public int DifficultyLevel { get; set; }

        public virtual TblActivity Activity { get; set; }
        public virtual TblDay Day { get; set; }
    }
}
