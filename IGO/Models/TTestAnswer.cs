using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TTestAnswer
    {
        public int FAnswerId { get; set; }
        public int? FQuestionId { get; set; }
        public string FAnswer { get; set; }
        public int? FScore { get; set; }

        public virtual TTestQuestion FQuestion { get; set; }
    }
}
