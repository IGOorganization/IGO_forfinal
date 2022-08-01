using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TCollectionGroup
    {
        public TCollectionGroup()
        {
            TCollectionGroupDetails = new HashSet<TCollectionGroupDetail>();
        }

        public int FCollectionGroupId { get; set; }
        public string FCollectionGroupName { get; set; }
        public int? FCustomerId { get; set; }

        public virtual ICollection<TCollectionGroupDetail> TCollectionGroupDetails { get; set; }
    }
}
