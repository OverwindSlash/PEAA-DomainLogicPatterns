using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionScript.DTOs
{
    public class ProductStockDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; }
        public short UnitsOnOrder { get; set; }
        public short UnitsNeedToPrepare { get; set; }
    }
}
