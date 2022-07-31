using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TCollectionGroupDetail
    {
        public int FCollectionGroupDetailId { get; set; }
        public int? FCollectionGroupId { get; set; }
        public int? FCollectionId { get; set; }

        public virtual TCollection FCollection { get; set; }
        public virtual TCollectionGroup FCollectionGroup { get; set; }
    }
}
