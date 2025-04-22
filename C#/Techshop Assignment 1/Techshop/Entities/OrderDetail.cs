using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techshop.Entities
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public Order Order { get; set; }    // Composition
        public Product Product { get; set; } // Composition
        public int Quantity { get; set; }

        public decimal CalculateSubtotal()
        {
            return Product.Price * Quantity;
        }

        public void GetOrderDetailInfo()
        {
            Console.WriteLine($"OrderDetail ID: {OrderDetailID}, Product: {Product.ProductName}, Quantity: {Quantity}, Subtotal: {CalculateSubtotal()}");
        }

        public void UpdateQuantity(int newQuantity)
        {
            Quantity = newQuantity;
        }

        public decimal AddDiscount(decimal discountPercentage)
        {
            decimal subtotal = CalculateSubtotal();
            decimal discountAmount = subtotal * (discountPercentage / 100);
            return subtotal - discountAmount;
        }
    }
}