using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTestProject.Domain;
using WPFTestProject.Service;

namespace UnitTestProject
{

    // unitest cannot connect to db
    // need to moq, add moq to references
    [TestClass]
    public class UnitTestWpf
    {
        [TestMethod]
        public void TestMethod1()
        {

            // need to moq the function that connect to db and fetches the data
            List<Customer> fakeCustomers = new List<Customer>();
            fakeCustomers.Add(new Customer { Name = "A" });
            fakeCustomers.Add(new Customer { Name = "A" });
            fakeCustomers.Add(new Customer { Name = "B" });

            var mock = new Mock<ICustomerService>();
            mock.Setup(x => x.GetCustomers()).Returns(fakeCustomers);

            CustomerBusinessService serviceTobeTested = new CustomerBusinessService(mock.Object);
            Assert.IsTrue(serviceTobeTested.GetCountOfNames("A") == 2);
        }

    }
}
