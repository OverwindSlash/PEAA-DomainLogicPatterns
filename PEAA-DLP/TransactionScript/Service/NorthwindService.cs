using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionScript.DTOs;
using TransactionScript.TableDataGateway;

namespace TransactionScript.Service
{
    public class NorthwindService
    {
        public void CalculateOrderDiscount(int orderId)
        {
            IList<OrderRelativeInfoDto> orderRelativeInfos = NorthwindTDG.FindOrderRelativeInfo(orderId).ToList();

            foreach (OrderRelativeInfoDto orderRelativeInfo in orderRelativeInfos)
            {

            }
        }
    }
}
