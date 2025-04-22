using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techshop.Entities
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public void CalculateTotalOrders()
        {
            Console.WriteLine("Calculating total orders for the customer...");
        }

        public void GetCustomerDetails()
        {
            Console.WriteLine($"ID: {CustomerID}, Name: {FirstName} {LastName}, Email: {Email}, Phone: {Phone}, Address: {Address}");
        }

        public void UpdateCustomerInfo(string newEmail, string newPhone, string newAddress)
        {
            Email = newEmail;
            Phone = newPhone;
            Address = newAddress;
        }
    }
}