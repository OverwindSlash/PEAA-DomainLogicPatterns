using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TableModule.DTOs;
using TableModule.TableDataGateway;

namespace TableModule.Service
{
    public class OrderDetailTM : AbstractTableModule
    {
        private const int DairyProductsCategory = 4;
        private const int MeatCategory = 6;
        private const int SeafoodCategory = 8;

        public override string TableName
        {
            get { return "OrderDetail"; }
        }

        public override string IdColumnName
        {
            get { return "OrderID"; }
        }

        public OrderDetailTM(DataSetHolder holder) : base(holder)
        {
        }

        public IList<OrderRelativeInfoDto> CalculateOrderDiscount(int orderId)
        {
            IList<OrderRelativeInfoDto> orderRelativeInfoDtos = new List<OrderRelativeInfoDto>();

            LoadOrderDetailById(orderId);
            DataRow[] orderDetailRows = this[orderId];

            foreach (DataRow orderDetailRow in orderDetailRows)
            {
                int productId = (int) orderDetailRow["ProductID"];

                ProductTM productTM = new ProductTM(this.Holder);
                productTM.LoadProductById(productId);
                DataRow[] productRows = productTM[productId];

                int categoryId = (int)productRows[0]["CategoryID"];
                CategoryTM categoryTM = new CategoryTM(this.Holder);
                categoryTM.LoadCategoryById(categoryId);
                DataRow[] categoryRows = categoryTM[categoryId];


                OrderRelativeInfoDto orderRelativeInfoDto = new OrderRelativeInfoDto();
                orderRelativeInfoDto.OrderID = orderId;
                orderRelativeInfoDto.ProductName = (string)productRows[0]["ProductName"];
                orderRelativeInfoDto.Quantity = (short)orderDetailRow["Quantity"];
                orderRelativeInfoDto.CategoryID = (int)categoryRows[0]["CategoryID"];
                orderRelativeInfoDto.CategoryName = (string)categoryRows[0]["CategoryName"];

                DoDiscountCalculation(orderRelativeInfoDto);

                orderRelativeInfoDtos.Add(orderRelativeInfoDto);
            }

            return orderRelativeInfoDtos;
        }

        public void LoadOrderDetailById(int orderId)
        {
            OrderDetailTDG orderDetailTDG = new OrderDetailTDG();
            IList<OrderDetailDto> orderDetailDtos = orderDetailTDG.FindOrderDetailById(orderId).ToList();

            DataTable orderDetailTable = new DataTable();
            orderDetailTable.TableName = "OrderDetail";

            Type orderDetailType = typeof(OrderDetailDto);
            PropertyInfo[] propertyInfos = orderDetailType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                DataColumn dataColumn = new DataColumn();
                dataColumn.ColumnName = propertyInfo.Name;
                dataColumn.DataType = propertyInfo.PropertyType;
                orderDetailTable.Columns.Add(dataColumn);
            }

            foreach (OrderDetailDto orderDetailDto in orderDetailDtos)
            {
                DataRow newRow = orderDetailTable.NewRow();

                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    newRow[propertyInfo.Name] = propertyInfo.GetValue(orderDetailDto);
                }

                orderDetailTable.Rows.Add(newRow);
            }

            Holder.AddTable(orderDetailTable);
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
    }
}
