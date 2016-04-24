using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Mapper;

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

        public void CalculateOrderDiscount1()
        {
            ProductMapper productMapper = new ProductMapper();
            Product product = productMapper.FindProductById(this.ProductID);

            CategoryMapper categoryMapper = new CategoryMapper();
            Category category = categoryMapper.FindCategoryById(product.CategoryID);

            DoDiscountCalculation(this, category);
        }

        private static void DoDiscountCalculation(OrderDetail orderDetail, Category category)
        {
            const int DairyProductsCategory = 4;
            const int MeatCategory = 6;
            const int SeafoodCategory = 8;

            orderDetail.Discount = 0.00f;

            if (category.CategoryID == DairyProductsCategory)
            {
                if (orderDetail.Quantity < 10)
                {
                    orderDetail.Discount = 0.00f;
                }
                else if (orderDetail.Quantity < 20)
                {
                    orderDetail.Discount = 0.05f;
                }
                else if (orderDetail.Quantity < 30)
                {
                    orderDetail.Discount = 0.10f;
                }
                else if (orderDetail.Quantity < 40)
                {
                    orderDetail.Discount = 0.15f;
                }
                else if (orderDetail.Quantity < 50)
                {
                    orderDetail.Discount = 0.20f;
                }
                else if (orderDetail.Quantity >= 50)
                {
                    orderDetail.Discount = 0.25f;
                }
            }

            if (category.CategoryID == MeatCategory)
            {
                if (orderDetail.Quantity <= 20)
                {
                    orderDetail.Discount = 0.00f;
                }
                else if (orderDetail.Quantity <= 40)
                {
                    orderDetail.Discount = 0.10f;
                }
                else if (orderDetail.Quantity <= 60)
                {
                    orderDetail.Discount = 0.20f;
                }
                else if (orderDetail.Quantity > 60)
                {
                    orderDetail.Discount = 0.30f;
                }
            }

            if (category.CategoryID == SeafoodCategory)
            {
                if (orderDetail.Quantity >= 10)
                {
                    orderDetail.Discount = 0.15f;
                }
            }
        }
    }
}
