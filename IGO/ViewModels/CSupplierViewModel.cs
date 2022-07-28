using IGO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CSupplierViewModel
    {
        public CSupplierViewModel()
        {
            _supplier = new TSupplier();
        }
        private TSupplier _supplier;
        public TSupplier supplier
        {
            get { return _supplier; }
            set { _supplier = value; }
        }

        public int FSupplierId
        {
            get { return _supplier.FSupplierId; }
            set { _supplier.FSupplierId = value; }

        }


        public string FCompanyName
        {
            get { return _supplier.FCompanyName; }
            set { _supplier.FCompanyName = value; }
        }

        public string FPhone
        {
            get { return _supplier.FPhone; }
            set { _supplier.FPhone = value; }
        }

        public string FAddress
        {
            get { return _supplier.FAddress; }
            set { _supplier.FAddress = value; }
        }

    }
}
