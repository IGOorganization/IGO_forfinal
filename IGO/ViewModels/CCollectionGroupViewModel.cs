using IGO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CCollectionGroupViewModel
    {
        DemoIgoContext _dbIgo;
        public CCollectionGroupViewModel(DemoIgoContext db)
        {
            _dbIgo = db;
        }

        public int CollectionGroupID { get; set; }
        
        public string CollectionGroupName
        {
            get
            {
                return _dbIgo.TCollectionGroups.FirstOrDefault(n => n.FCollectionGroupId == CollectionGroupID).FCollectionGroupName;
            }
        }
    }
}
