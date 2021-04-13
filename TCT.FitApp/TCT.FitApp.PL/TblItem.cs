using System;
using System.Collections.Generic;

#nullable disable

namespace TCT.FitApp.PL
{
    public partial class TblItem
    {
        public TblItem()
        {
            TblDayItems = new HashSet<TblDayItem>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TypeId { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public Guid? UserId { get; set; }

        public virtual TblItemType Type { get; set; }
        public virtual ICollection<TblDayItem> TblDayItems { get; set; }
    }
}
