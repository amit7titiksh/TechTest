using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnyCompany.DataAccess;
using AnyCompany.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany.Tests
{
    [TestClass()]
    public class CustomerRepositoryTests
    {
        [TestMethod()]
        public void WithNOCustomerID()
        {
           Customer customer =  CustomerRepository.Load(-1);
  
            Assert.AreEqual(null, customer);
        }

        [TestMethod()]
        public void WithCustomerIDAndWorngConnectionStringSetup() // assume no connection setup
        {
            Customer customer = CustomerRepository.Load(100);
            Assert.AreEqual(null, customer);
        }
    }
}
