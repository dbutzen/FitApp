using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCT.FitApp.Mobile.Models
{
    public class DayActivity
    {
        public Guid Id { get; set; }
        public Guid DayId { get; set; }
        public Guid ActivityId { get; set; }
        public int Duration { get; set; }
        public int DifficultyLevel { get; set; }
    }
}
