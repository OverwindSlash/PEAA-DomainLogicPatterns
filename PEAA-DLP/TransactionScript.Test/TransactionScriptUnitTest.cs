using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransactionScript.Service;

namespace TransactionScript.Test
{
    [TestClass]
    public class TransactionScriptUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            int orderId = 10693;

            NorthwindService service = new NorthwindService();
            service.CalculateOrderDiscount(orderId);
        }
    }
}
