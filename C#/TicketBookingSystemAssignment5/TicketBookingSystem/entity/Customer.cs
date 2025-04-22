using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.entity
{
    public class Customer
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Default Constructor
        public Customer()
        {
        }

        // Overloaded Constructor
        public Customer(string customerName, string email, string phoneNumber)
        {
            CustomerName = customerName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        // Display method
        public void DisplayCustomerDetails()
        {
            Console.WriteLine($"\nCustomer Name: {CustomerName}\nEmail: {Email}\nPhone: {PhoneNumber}\n");
        }
    }
}