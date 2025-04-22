using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techshop.Entities;

namespace Techshop.DAO
{
    public interface ICustomerDAO
    {
        void AddCustomer(Customer customer);
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int customerId);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int customerId);
    }
}