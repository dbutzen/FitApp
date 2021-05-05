using System;
using System.Collections.Generic;
using System.Text;

namespace TCT.FitApp.Mobile.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid UniqueKey { get; set; }
        public Guid? SessionKey { get; set; } // Use for logging in
        public int CalorieGoal { get; set; }
        public int ProteinGoal { get; set; }
        public int DaysInARowSucceeded { get; set; }
        public int HeightInches { get; set; }
        public int WeightPounds { get; set; }
        public Guid UserAccessLevelId { get; set; }
        public string Sex { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
