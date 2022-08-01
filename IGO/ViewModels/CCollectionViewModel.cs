using IGO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CCollectionViewModel
    {
        private readonly DemoIgoContext _db;

        private TCollection _collection;

        public CCollectionViewModel(DemoIgoContext db)
        {
            _collection = new TCollection();
            _db = db;
        }

        public TCollection collection
        {
            get { return _collection; }
            set
            {
                _collection = value;

            }
        }

        public int collectionid
        {
            get { return _collection.FCollectionId; }
            set { _collection.FCollectionId = value; }
        }

        public int customerid
        {
            get { return _collection.FCollectionId; }
            set { _collection.FCustomerId = value; }
        }


        public int? productid
        {
            get { return _collection.FProductId; }
            set { _collection.FProductId = value; }
        }


        public string collectionDate
        {
            get { return _collection.FCollectionDate; }
            set { _collection.FCollectionDate = value; }
        }

        public int?  movieid
        {
            get { return _collection.FMovieId; }
            set { _collection.FMovieId= value; }
        }
        public CProductViewModel VMproduct;


    }
}
