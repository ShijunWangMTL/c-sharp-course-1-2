using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTestProject.Domain;

namespace WPFTestProject.Service
{
    public class CustomerBusinessService
    {
        ICustomerService customerService;
        public CustomerBusinessService(ICustomerService service)
        {
            customerService = service;
        }

        public int GetCountOfNames(string name)
        {
            List<Customer> customers = customerService.GetCustomers();
            return customers.Where(cs => cs.Name == name).Count();
        }
    }
}
