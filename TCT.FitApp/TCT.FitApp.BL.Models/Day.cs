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
        public List<Activity> Activities { get; set; }
        public List<Item> Items { get; set; }
        public bool Succeeded { get; set; }

        //public int CaloriesConsumed { get { return Items.Sum(i => i.Servings * i.Calories); } }
        //public double CaloriesBurned { get { return Activities.Sum(i => i.CaloriesBurned); } }
        //public int ProteinConsumed { get { return Items.Sum(i => i.Servings * i.Protein); } }


        // For the reporting
        public int CaloriesConsumed { get; set; }
        public double CaloriesBurned { get; set; }
        public int ProteinConsumed { get; set; }

    }
}
