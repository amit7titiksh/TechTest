using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnyCompany.DataAccess;
using AnyCompany.Entities;
using AnyCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        OrderService orderService = null;
        [TestInitialize]
        public void Initialize()
        {
            var parameter = /*...test data that i need in all tests...*/
            this.orderService = new OrderService();
        }

        [TestMethod()]
        public void WithNOCustomerID()
        {
            Order order = new Order();
            bool result = orderService.PlaceOrder(order, -1);

            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void WithCustomerIdNotinSystem()
        {
            Order order = new Order();
            bool result = orderService.PlaceOrder(order, 9999999);// imagine 9999999  is not there in system as customer id

            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void WithOrderAmountZero()
        {
            Order order = new Order() { OrderId = 123, Amount = 0 };
            bool result = orderService.PlaceOrder(order, 2);// imagine 9999999  is not there in system as customer id

            Assert.AreEqual(false, result);
        }

        [TestMethod()]
        public void WithSavedOrder() // assuming database connection setup correctly
        {
            Order order = new Order() { OrderId = 123, Amount = 120 };
            bool result = orderService.PlaceOrder(order, 2);// imagine 9999999  is not there in system as customer id

            Assert.AreEqual(true, result);
        }
    }
}
