using System;
using System.Collections.Generic;

#nullable disable

namespace TCT.FitApp.PL
{
    public partial class tblUserAccessLevel
    {
        public tblUserAccessLevel()
        {
            TblUsers = new HashSet<tblUser>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<tblUser> TblUsers { get; set; }
    }
}
