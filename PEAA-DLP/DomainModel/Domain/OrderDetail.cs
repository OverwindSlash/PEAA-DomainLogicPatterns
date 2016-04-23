using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Domain
{
    public class OrderDetail : DomainObject
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public override string DomainId
        {
            get { return String.Format("{0}:{1}", OrderID, ProductID); }
        }
    }
}
