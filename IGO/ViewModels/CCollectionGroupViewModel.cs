using IGO.Models;
using Microsoft.EntityFrameworkCore;
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

        public string PhotoPath
        {
            get
            {
                int p = 0;
                string path = "a-1.jpg";
                if (_dbIgo.TCollectionGroupDetails.Include(n => n.FCollection).FirstOrDefault(n => n.FCollectionGroupId == CollectionGroupID) != null)
                {
                    p = (int)_dbIgo.TCollectionGroupDetails.Include(n => n.FCollection).FirstOrDefault(n => n.FCollectionGroupId == CollectionGroupID).FCollection.FProductId;
                    path = _dbIgo.TProductsPhotos.FirstOrDefault(n => n.FProductId == p && n.FPhotoSiteId == 3).FPhotoPath;
                }
                return path;
            }
        }
        public int IncludeNum
        {
            get
            {
                return _dbIgo.TCollectionGroupDetails.Where(n => n.FCollectionGroupId == CollectionGroupID).Count();
            }
        }
    }
}
