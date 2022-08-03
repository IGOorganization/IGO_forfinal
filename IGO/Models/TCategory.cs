using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TCategory
    {
        public TCategory()
        {
            TSubCategories = new HashSet<TSubCategory>();
        }

        public int FCategoryId { get; set; }
        public string FCategoryName { get; set; }
        public string FCategoryPhotoPath { get; set; }

        public virtual ICollection<TSubCategory> TSubCategories { get; set; }
    }
}
