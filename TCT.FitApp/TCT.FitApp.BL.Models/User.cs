using System;
using System.Collections.Generic;
using System.Text;

namespace TCT.FitApp.BL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid UniqueKey { get; set; }
        public int CalorieGoal { get; set; }
        public int ProteinGoal { get; set; }
        public int DaysInARowSucceeded { get; set; }
        public int HeightInches { get; set; }
        public int WeightPounds { get; set; }
        public Guid UserAccessLevel { get; set; }
        public string Sex { get; set; }
    }
}
