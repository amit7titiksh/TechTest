using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnyCompany.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyCompany.Entities;

namespace AnyCompany.DataAccess.Tests
{
    [TestClass()]
    public class OrderRepositoryTests
    {
        [TestMethod()]
        public void NoOrderedEntered()
        {
            //Order order = new Order() { Amount = 120, OrderId = 23, CustomerID = 123, VAT = 0.0 };

            OrderRepository orderRepository = new OrderRepository();
            Assert.AreEqual(false, orderRepository.SavedStatus);
        }

        [TestMethod()]
        public void OrderedEntered()// assuming connection string is setup correctly
        {
            Order order = new Order() { Amount = 120, OrderId = 23, CustomerID = 10, VAT = 0.0 };

            OrderRepository orderRepository = new OrderRepository();
            orderRepository.Save(order);
            Assert.AreEqual(true, orderRepository.SavedStatus);
        }
    }
}