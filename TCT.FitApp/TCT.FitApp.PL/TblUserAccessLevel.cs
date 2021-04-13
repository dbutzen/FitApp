using System;
using System.Collections.Generic;

#nullable disable

namespace TCT.FitApp.PL
{
    public partial class TblUserAccessLevel
    {
        public TblUserAccessLevel()
        {
            TblUsers = new HashSet<TblUser>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}
