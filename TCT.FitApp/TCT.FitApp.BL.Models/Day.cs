using System;
using System.Collections.Generic;
using System.Text;

namespace TCT.FitApp.BL.Models
{
    public class Day
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<Activity> Activities { get; set; }
        public List<Item> Items { get; set; }
        public bool Succeeded { get; set; }


    }
}
