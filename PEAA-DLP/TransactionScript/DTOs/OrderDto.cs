using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace TransactionScript.DTOs
{
    public class OrderDto
    {
        public int OrderID { get; set; }
        public int EmployeeID { get; set; }
    }
}
