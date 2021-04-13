using System;
using System.Collections.Generic;

#nullable disable

namespace TCT.FitApp.PL
{
    public partial class TblItemType
    {
        public TblItemType()
        {
            TblItems = new HashSet<TblItem>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TblItem> TblItems { get; set; }
    }
}
