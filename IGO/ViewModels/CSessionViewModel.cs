using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CSessionViewModel
    {
       public int UserId { get; set; }
       public List<int> ShoppingCartItems { get; set; }
    }
}
