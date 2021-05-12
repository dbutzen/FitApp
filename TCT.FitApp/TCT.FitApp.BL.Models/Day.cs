using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace TCT.FitApp.BL.Models
{
    public class Day
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<Activity> Activities { get; set; } = new List<Activity>();
        public List<Item> Items { get; set; } = new List<Item>();
        public bool Succeeded { get; set; }

        //public int CaloriesConsumed { get { return Items.Sum(i => i.Servings * i.Calories); } }
        //public double CaloriesBurned { get { return Activities.Sum(i => i.CaloriesBurned); } }
        //public int ProteinConsumed { get { return Items.Sum(i => i.Servings * i.Protein); } }


        // For the reporting

        public int ActivityCount { get; set; }
        public int CaloriesConsumed { get; set; }
        public double CaloriesBurned { get; set; }
        public int ProteinConsumed { get; set; }

    }
}
