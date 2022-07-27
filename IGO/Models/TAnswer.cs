using System;
using System.Collections.Generic;

#nullable disable

namespace IGO.Models
{
    public partial class TAnswer
    {
        public int FAnswerId { get; set; }
        public int? FQuestionId { get; set; }
        public string FAnswer { get; set; }
    }
}
