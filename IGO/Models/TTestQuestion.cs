using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TTestQuestion
    {
        public TTestQuestion()
        {
            TTestAnswers = new HashSet<TTestAnswer>();
        }

        public int FQuestionId { get; set; }
        public string FQuestion { get; set; }

        public virtual ICollection<TTestAnswer> TTestAnswers { get; set; }
    }
}
