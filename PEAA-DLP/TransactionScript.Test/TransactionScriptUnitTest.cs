using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransactionScript.DTOs;
using TransactionScript.Service;

namespace TransactionScript.Test
{
    [TestClass]
    public class TransactionScriptUnitTest
    {
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
