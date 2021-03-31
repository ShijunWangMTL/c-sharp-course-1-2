using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTestProject.Domain;

namespace WPFTestProject.Service
{
    class CarService
    {

        public int getAvgCustomers(List<Customer> customers)
        {
            return 1;
        }

        public int getCarsCount(Customer c)
        {
            // it connects to db to fetch the data
            //using the data container
            // need to mock(moq) the call to db
            // need use interface
            return 1;
        }
    }
}
