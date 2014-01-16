using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts.EntityDomainLibrary
{
    public class Products_Suppliers
    {
        private int productSupplierId;
        private int productId;
        private int supplierId;

        public int ProductSupplierId
        {
            get { return productSupplierId; }
            set { productSupplierId = value; }
        }

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        public int SupplierId
        {
          get { return supplierId; }
          set { supplierId = value; }
        }

        public Products_Suppliers() { }
    }
}
