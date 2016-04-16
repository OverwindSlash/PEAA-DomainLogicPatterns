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

        public IList<OrderRelativeInfoDto> CalculateOrderDiscount1(int orderId)
        {
            IList<OrderRelativeInfoDto> orderRelativeInfoDtos = new List<OrderRelativeInfoDto>();

            IList<OrderDetailDto> orderDetailDtos = NorthwindTDG.FindOrderDetailById(orderId).ToList();
            foreach (OrderDetailDto orderDetailDto in orderDetailDtos)
            {
                ProductDto productDto = NorthwindTDG.FindProductById(orderDetailDto.ProductID).SingleOrDefault();
                CategoryDto categoryDto = NorthwindTDG.FindCategoryById(productDto.CategoryID).SingleOrDefault();

                OrderRelativeInfoDto orderRelativeInfoDto = new OrderRelativeInfoDto();
                orderRelativeInfoDto.OrderID = orderId;
                orderRelativeInfoDto.ProductName = productDto.ProductName;
                orderRelativeInfoDto.Quantity = orderDetailDto.Quantity;
                orderRelativeInfoDto.CategoryID = productDto.CategoryID;
                orderRelativeInfoDto.CategoryName = categoryDto.CategoryName;

                DoDiscountCalculation(orderRelativeInfoDto);

                orderRelativeInfoDtos.Add(orderRelativeInfoDto);
            }

            return orderRelativeInfoDtos;
        }

        public IList<OrderRelativeInfoDto> CalculateOrderDiscount2(int orderId)
        {
            IList<OrderRelativeInfoDto> orderRelativeInfoDtos = NorthwindTDG.FindOrderRelativeInfo(orderId).ToList();

            foreach (OrderRelativeInfoDto orderRelativeInfo in orderRelativeInfoDtos)
            {
                DoDiscountCalculation(orderRelativeInfo);
            }

            return orderRelativeInfoDtos;
        }

        private static void DoDiscountCalculation(OrderRelativeInfoDto orderRelativeInfo)
        {
            if (!orderRelativeInfo.CategoryID.HasValue)
            {
                return;
            }

            orderRelativeInfo.Discount = 0.00f;

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
                else if (orderRelativeInfo.Quantity < 40)
                {
                    orderRelativeInfo.Discount = 0.15f;
                }
                else if (orderRelativeInfo.Quantity < 50)
                {
                    orderRelativeInfo.Discount = 0.20f;
                }
                else if (orderRelativeInfo.Quantity >= 50)
                {
                    orderRelativeInfo.Discount = 0.25f;
                }
            }

            if (orderRelativeInfo.CategoryID.Value == MeatCategory)
            {
                if (orderRelativeInfo.Quantity <= 20)
                {
                    orderRelativeInfo.Discount = 0.00f;
                }
                else if (orderRelativeInfo.Quantity <= 40)
                {
                    orderRelativeInfo.Discount = 0.10f;
                }
                else if (orderRelativeInfo.Quantity <= 60)
                {
                    orderRelativeInfo.Discount = 0.20f;
                }
                else if (orderRelativeInfo.Quantity > 60)
                {
                    orderRelativeInfo.Discount = 0.30f;
                }
            }

            if (orderRelativeInfo.CategoryID.Value == SeafoodCategory)
            {
                if (orderRelativeInfo.Quantity >= 10)
                {
                    orderRelativeInfo.Discount = 0.15f;
                }
            }
        }

        public IList<EmployeeDto> CalculateOrderQuantityByEmployee()
        {
            IList<EmployeeDto> employeeDtos = NorthwindTDG.FindAllEmployees().ToList();

            foreach (EmployeeDto employeeDto in employeeDtos)
            {
                IList<OrderDto> orderDtos = NorthwindTDG.FindOrdersByEmployeeID(employeeDto.EmployeeID).ToList();
                employeeDto.OrderQuantity = orderDtos.Count;
            }

            return employeeDtos;
        }
    }
}
