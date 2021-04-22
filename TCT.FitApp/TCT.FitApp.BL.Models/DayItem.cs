using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCT.FitApp.BL.Models
{
    public class DayItem
    {
        public Guid Id { get; set; }
        public Guid DayId { get; set; }
        public Guid ItemId { get; set; }
        public int Servings { get; set; }
    }
}