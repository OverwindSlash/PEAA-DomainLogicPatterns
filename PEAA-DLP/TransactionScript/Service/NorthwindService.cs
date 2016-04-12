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
        private const int DairyProductsCategory = 4;
        private const int MeatCategory = 6;
        private const int SeafoodCategory = 8;

        public void CalculateOrderDiscount(int orderId)
        {
            IList<OrderRelativeInfoDto> orderRelativeInfos = NorthwindTDG.FindOrderRelativeInfo(orderId).ToList();

            foreach (OrderRelativeInfoDto orderRelativeInfo in orderRelativeInfos)
            {
                if (!orderRelativeInfo.CategoryID.HasValue)
                {
                    continue;
                }

                if (orderRelativeInfo.CategoryID.Value == DairyProductsCategory)
                {
                    if (orderRelativeInfo.Quantity < 10)
                    {
                        orderRelativeInfo.Discount = 0.00f;
                    }
                    else if (orderRelativeInfo.Quantity < 20)
                    {
                        orderRelativeInfo.Discount = 0.05f;
                    }
                    else if (orderRelativeInfo.Quantity < 30)
                    {
                        orderRelativeInfo.Discount = 0.10f;
                    }
                    else if (orderRelativeInfo.Quantity >= 30)
                    {
                        orderRelativeInfo.Discount = 0.15f;
                    }
                }

                if (orderRelativeInfo.CategoryID.Value == MeatCategory)
                {
                    if (orderRelativeInfo.Quantity <= 10)
                    {
                        orderRelativeInfo.Discount = 0.00f;
                    }
                    else if (orderRelativeInfo.Quantity <= 20)
                    {
                        orderRelativeInfo.Discount = 0.05f;
                    }
                    else if (orderRelativeInfo.Quantity <= 40)
                    {
                        orderRelativeInfo.Discount = 0.10f;
                    }
                    else if (orderRelativeInfo.Quantity > 40)
                    {
                        orderRelativeInfo.Discount = 0.15f;
                    }
                }

                if (orderRelativeInfo.CategoryID.Value == SeafoodCategory)
                {
                    if (orderRelativeInfo.Quantity <= 5)
                    {
                        orderRelativeInfo.Discount = 0.05f;
                    }
                    else if (orderRelativeInfo.Quantity <= 10)
                    {
                        orderRelativeInfo.Discount = 0.10f;
                    }
                    else if (orderRelativeInfo.Quantity > 10)
                    {
                        orderRelativeInfo.Discount = 0.15f;
                    }
                }
            }
        }
    }
}
