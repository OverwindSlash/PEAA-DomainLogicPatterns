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
            Assert.AreEqual(0.00f, orderRelativeInfoDtos[0].Discount);
        }
    }
}
