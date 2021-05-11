using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCT.FitApp.Mobile.Models
{
    public class Day
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<Activity> Activities { get; set; } = new List<Activity>();
        public List<Item> Items { get; set; } = new List<Item>();
        public bool Succeeded { get; set; }

        public int CaloriesConsumed { get { return Items.Sum(i => i.Servings * i.Calories); } }
        public double CaloriesBurned { get { return Activities.Sum(i => i.CaloriesBurned); } }
        public int ProteinConsumed { get { return Items.Sum(i => i.Servings * i.Protein); } }


        // For the reporting
        public int _CaloriesConsumed { get; set; }
        public double _CaloriesBurned { get; set; }
        public int _ProteinConsumed { get; set; }

    }
}
