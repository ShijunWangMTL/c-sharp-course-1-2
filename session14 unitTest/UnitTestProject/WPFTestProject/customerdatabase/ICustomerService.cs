using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTestProject.Domain;

namespace WPFTestProject.Service
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers();
        int GetCountOfNames(string name);
        Customer AddCustomer(Customer c);
    }
}
