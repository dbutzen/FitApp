using System;
using System.Collections.Generic;

#nullable disable

namespace TCT.FitApp.PL
{
    public partial class tblItemType
    {
        public tblItemType()
        {
            TblItems = new HashSet<tblItem>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<tblItem> TblItems { get; set; }
    }
}
