using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TCollectionGroupDetail
    {
        public int FCollectionGroupDetail { get; set; }
        public int? FCollectionGroupId { get; set; }
        public int? FCollection { get; set; }

        public virtual TCollectionGroup FCollectionGroup { get; set; }
    }
}
