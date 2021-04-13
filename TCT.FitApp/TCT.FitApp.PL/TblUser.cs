using System;
using System.Collections.Generic;

#nullable disable

namespace TCT.FitApp.PL
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblDays = new HashSet<TblDay>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid UniqueKey { get; set; }
        public int CalorieGoal { get; set; }
        public int ProteinGoal { get; set; }
        public int DaysInArowSucceeded { get; set; }
        public int HeightInches { get; set; }
        public int WeightPounds { get; set; }
        public Guid UserAccessLevelId { get; set; }
        public string Sex { get; set; }

        public virtual TblUserAccessLevel UserAccessLevel { get; set; }
        public virtual ICollection<TblDay> TblDays { get; set; }
    }
}
