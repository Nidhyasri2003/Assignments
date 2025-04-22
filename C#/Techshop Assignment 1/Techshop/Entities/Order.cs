using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techshop.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public Customer Customer { get; set; }  // Composition
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        public void CalculateTotalAmount()
        {
            Console.WriteLine("Calculating total order amount...");
        }

        public void GetOrderDetails()
        {
            Console.WriteLine($"Order ID: {OrderID}, Customer: {Customer.FirstName} {Customer.LastName}, Date: {OrderDate}, Total: {TotalAmount}");
        }

        public void UpdateOrderStatus(string newStatus)
        {
            Status = newStatus;
        }

        public void CancelOrder()
        {
            Status = "Cancelled";
            Console.WriteLine("Order has been cancelled.");
        }
    }
}