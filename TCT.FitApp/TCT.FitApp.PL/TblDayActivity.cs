using System;
using System.Collections.Generic;

#nullable disable

namespace TCT.FitApp.PL
{
    public partial class tblDayActivity
    {
        public Guid Id { get; set; }
        public Guid DayId { get; set; }
        public Guid ActivityId { get; set; }
        public int Duration { get; set; }
        public int DifficultyLevel { get; set; }

        public virtual tblActivity Activity { get; set; }
        public virtual tblDay Day { get; set; }
    }
}
