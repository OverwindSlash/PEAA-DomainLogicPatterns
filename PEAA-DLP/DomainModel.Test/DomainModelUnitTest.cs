using System;
using System.Collections.Generic;
using DomainModel.Domain;
using DomainModel.Mapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainModel.Test
{
    [TestClass]
    public class DomainModelUnitTest
    {
        [TestMethod]
        public void TestFindOrderDetailById()
        {
            int orderId = 10693;
            int productId = 9;

            OrderDetailMapper orderDetailMapper = new OrderDetailMapper();

            OrderDetail orderDetail = orderDetailMapper.FindOrderDetailById(orderId, productId);

            Assert.AreEqual(10693, orderDetail.OrderID);
            Assert.AreEqual(9, orderDetail.ProductID);
            Assert.AreEqual(6, orderDetail.Quantity);
            Assert.AreEqual(0f, orderDetail.Discount);
            Assert.AreEqual("10693:9", orderDetail.DomainId);
        }

        [TestMethod]
        public void TestReFindOrderDetailByIdUseIdentityMap()
        {
            int orderId = 10693;
            int productId = 9;

            OrderDetailMapper orderDetailMapper = new OrderDetailMapper();

            OrderDetail orderDetail = orderDetailMapper.FindOrderDetailById(orderId, productId);

            Assert.AreEqual(10693, orderDetail.OrderID);
            Assert.AreEqual(9, orderDetail.ProductID);
            Assert.AreEqual(6, orderDetail.Quantity);
            Assert.AreEqual(0f, orderDetail.Discount);
            Assert.AreEqual("10693:9", orderDetail.DomainId);

            OrderDetail orderDetailFromIdentityMap = orderDetailMapper.FindOrderDetailById(orderId, productId);

            Assert.AreEqual(orderDetail, orderDetailFromIdentityMap);
        }


        [TestMethod]
        public void TestFindOrderDetailByCriterion()
        {
            int orderId = 10693;

            OrderDetailMapper orderDetailMapper = new OrderDetailMapper();

            IList<OrderDetail> orderDetails = orderDetailMapper.FindOrderDetailByCriterion(orderId);

            Assert.AreEqual(4, orderDetails.Count);

            Assert.AreEqual(orderId, orderDetails[0].OrderID);
            Assert.AreEqual(9, orderDetails[0].ProductID);
            Assert.AreEqual(6, orderDetails[0].Quantity);
            Assert.AreEqual(0f, orderDetails[0].Discount);

            Assert.AreEqual(orderId, orderDetails[1].OrderID);
            Assert.AreEqual(54, orderDetails[1].ProductID);
            Assert.AreEqual(60, orderDetails[1].Quantity);
            Assert.AreEqual(0.15f, orderDetails[1].Discount);

            Assert.AreEqual(orderId, orderDetails[2].OrderID);
            Assert.AreEqual(69, orderDetails[2].ProductID);
            Assert.AreEqual(30, orderDetails[2].Quantity);
            Assert.AreEqual(0.15f, orderDetails[2].Discount);

            Assert.AreEqual(orderId, orderDetails[3].OrderID);
            Assert.AreEqual(73, orderDetails[3].ProductID);
            Assert.AreEqual(15, orderDetails[3].Quantity);
            Assert.AreEqual(0.15f, orderDetails[3].Discount);
        }

        [TestMethod]
        public void TestReFindOrderDetailByCriterionUseIdentityMap()
        {
            int orderId = 10693;

            OrderDetailMapper orderDetailMapper = new OrderDetailMapper();

            IList<OrderDetail> orderDetails = orderDetailMapper.FindOrderDetailByCriterion(orderId);

            IList<OrderDetail> orderDetailsFromIdentityMap = orderDetailMapper.FindOrderDetailByCriterion(orderId);

            for (int i = 0; i < orderDetails.Count; i++)
            {
                Assert.AreEqual(orderDetails[i], orderDetailsFromIdentityMap[i]);
            }


            int productId = 9;

            OrderDetail orderDetailSingle = orderDetailMapper.FindOrderDetailById(orderId, productId);

            Assert.AreEqual(orderDetails[0], orderDetailSingle);
        }

        [TestMethod]
        public void TestFindProductById()
        {
            int productId = 9;

            ProductMapper productMapper = new ProductMapper();

            Product product = productMapper.FindProductById(productId);

            Assert.AreEqual(productId, product.ProductID);
            Assert.AreEqual("Mishi Kobe Niku", product.ProductName);
            Assert.AreEqual(6, product.CategoryID);
            Assert.AreEqual(productId.ToString(), product.DomainId);
        }

        [TestMethod]
        public void TestReFindProductByIdUsingIdentityMap()
        {
            int productId = 9;

            ProductMapper productMapper = new ProductMapper();

            Product product = productMapper.FindProductById(productId);

            Product productFromIdentityMap = productMapper.FindProductById(productId);

            Assert.AreEqual(product, productFromIdentityMap);
        }


        [TestMethod]
        public void TestFindCategoryById()
        {
            int categoryId = 6;

            CategoryMapper categoryMapper = new CategoryMapper();

            Category category = categoryMapper.FindCategoryById(6);

            Assert.AreEqual(categoryId, category.CategoryID);
            Assert.AreEqual("Meat/Poultry", category.CategoryName);
            Assert.AreEqual(categoryId.ToString(), category.DomainId);
        }

    }
}
