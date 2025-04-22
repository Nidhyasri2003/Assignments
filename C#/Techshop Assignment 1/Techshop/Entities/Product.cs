using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techshop.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public void GetProductDetails()
        {
            Console.WriteLine($"ID: {ProductID}, Name: {ProductName}, Description: {Description}, Price: {Price}");
        }

        public void UpdateProductInfo(string newDescription, decimal newPrice)
        {
            Description = newDescription;
            Price = newPrice;
        }

        public bool IsProductInStock(int quantityInStock)
        {
            return quantityInStock > 0;
        }
    }
}