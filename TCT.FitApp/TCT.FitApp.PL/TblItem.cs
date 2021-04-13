using System;
using System.Collections.Generic;

#nullable disable

namespace TCT.FitApp.PL
{
    public partial class tblItem
    {
        public tblItem()
        {
            TblDayItems = new HashSet<tblDayItem>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TypeId { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public Guid? UserId { get; set; }

        public virtual tblItemType Type { get; set; }
        public virtual ICollection<tblDayItem> TblDayItems { get; set; }
    }
}
