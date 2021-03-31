using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTestProject.Domain;

namespace WPFTestProject.Service
{
    public class CustomerService : ICustomerService
    {
        public List<Customer> GetCustomers()
        {
            return Global.ctx.customers.ToList();
        }



        public Customer AddCustomer(Customer c)
        {
            try {
                Global.ctx.customers.Add(c);
                Global.ctx.SaveChanges();
            }
            catch(SystemException exc)
            {
                throw exc;
            }

            return Global.ctx.customers.Where(customer => customer.Name == c.Name).FirstOrDefault();

        }

        public int GetCountOfNames(string name)
        {
            throw new NotImplementedException();
        }
    }
}
