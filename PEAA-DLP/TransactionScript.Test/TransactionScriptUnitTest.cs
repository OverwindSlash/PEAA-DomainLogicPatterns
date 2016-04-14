using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransactionScript.DTOs;
using TransactionScript.Service;
using TransactionScript.TableDataGateway;

namespace TransactionScript.Test
{
    [TestClass]
    public class TransactionScriptUnitTest
    {
        [TestMethod]
        public void TestTDGFindOrderDetailById()
        {
            int orderId = 10693;

            IList<OrderDetailDto> orderDetailDtos = NorthwindTDG.FindOrderDetailById(orderId).ToList();

            Assert.AreEqual(4, orderDetailDtos.Count);

            Assert.AreEqual(orderId, orderDetailDtos[0].OrderID);
            Assert.AreEqual(9, orderDetailDtos[0].ProductID);
            Assert.AreEqual(6, orderDetailDtos[0].Quantity);
            Assert.AreEqual(0f, orderDetailDtos[0].Discount);

            Assert.AreEqual(orderId, orderDetailDtos[1].OrderID);
            Assert.AreEqual(54, orderDetailDtos[1].ProductID);
            Assert.AreEqual(60, orderDetailDtos[1].Quantity);
            Assert.AreEqual(0.15f, orderDetailDtos[1].Discount);

            Assert.AreEqual(orderId, orderDetailDtos[2].OrderID);
            Assert.AreEqual(69, orderDetailDtos[2].ProductID);
            Assert.AreEqual(30, orderDetailDtos[2].Quantity);
            Assert.AreEqual(0.15f, orderDetailDtos[2].Discount);

            Assert.AreEqual(orderId, orderDetailDtos[3].OrderID);
            Assert.AreEqual(73, orderDetailDtos[3].ProductID);
            Assert.AreEqual(15, orderDetailDtos[3].Quantity);
            Assert.AreEqual(0.15f, orderDetailDtos[3].Discount);
        }

        [TestMethod]
        public void TestTDGFindProductById()
        {
            int orderId = 10693;

            IList<OrderDetailDto> orderDetailDtos = NorthwindTDG.FindOrderDetailById(orderId).ToList();

            int productId1 = orderDetailDtos[0].ProductID;
            ProductDto productDto1 = NorthwindTDG.FindProductById(productId1).SingleOrDefault();
            Assert.AreEqual(productId1, productDto1.ProductID);
            Assert.AreEqual("Mishi Kobe Niku", productDto1.ProductName);
            Assert.AreEqual(6, productDto1.CategoryID);

            int productId2 = orderDetailDtos[1].ProductID;
            ProductDto productDto2 = NorthwindTDG.FindProductById(productId1).SingleOrDefault();
            Assert.AreEqual(productId2, productDto2.ProductID);
            Assert.AreEqual("Tourtière", productDto2.ProductName);
            Assert.AreEqual(6, productDto2.CategoryID);
        }


        [TestMethod]
        public void TestCalculateOrderDiscount()
        {
            int orderId = 10693;

            NorthwindService service = new NorthwindService();
            IList<OrderRelativeInfoDto> orderRelativeInfoDtos = service.CalculateOrderDiscount(orderId);

            Assert.AreEqual(4, orderRelativeInfoDtos.Count);

            Assert.AreEqual("Mishi Kobe Niku", orderRelativeInfoDtos[0].ProductName);
            Assert.AreEqual("Meat/Poultry", orderRelativeInfoDtos[0].CategoryName);
            Assert.AreEqual(6, orderRelativeInfoDtos[0].Quantity);
            Assert.AreEqual(0.00f, orderRelativeInfoDtos[0].Discount);

            Assert.AreEqual("Tourtière", orderRelativeInfoDtos[1].ProductName);
            Assert.AreEqual("Meat/Poultry", orderRelativeInfoDtos[1].CategoryName);
            Assert.AreEqual(60, orderRelativeInfoDtos[1].Quantity);
            Assert.AreEqual(0.20f, orderRelativeInfoDtos[1].Discount);

            Assert.AreEqual("Gudbrandsdalsost", orderRelativeInfoDtos[2].ProductName);
            Assert.AreEqual("Dairy Products", orderRelativeInfoDtos[2].CategoryName);
            Assert.AreEqual(30, orderRelativeInfoDtos[2].Quantity);
            Assert.AreEqual(0.15f, orderRelativeInfoDtos[2].Discount);

            Assert.AreEqual("Röd Kaviar", orderRelativeInfoDtos[3].ProductName);
            Assert.AreEqual("Seafood", orderRelativeInfoDtos[3].CategoryName);
            Assert.AreEqual(15, orderRelativeInfoDtos[3].Quantity);
            Assert.AreEqual(0.15f, orderRelativeInfoDtos[3].Discount);
        }
    }
}
