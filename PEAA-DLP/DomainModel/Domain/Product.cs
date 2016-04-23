using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Domain
{
    public class Product : DomainObject
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }

        public override string DomainId
        {
            get { return ProductID.ToString(); }
        }
    }
}
