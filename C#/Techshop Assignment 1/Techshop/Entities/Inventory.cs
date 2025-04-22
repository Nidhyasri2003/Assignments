using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techshop.Entities
{
    public class Inventory
    {
        public int InventoryID { get; set; }
        public Product Product { get; set; }  // Composition
        public int QuantityInStock { get; set; }
        public DateTime LastStockUpdate { get; set; }

        public Product GetProduct()
        {
            return Product;
        }

        public int GetQuantityInStock()
        {
            return QuantityInStock;
        }

        public void AddToInventory(int quantity)
        {
            QuantityInStock += quantity;
        }

        public void RemoveFromInventory(int quantity)
        {
            QuantityInStock -= quantity;
        }

        public void UpdateStockQuantity(int newQuantity)
        {
            QuantityInStock = newQuantity;
        }

        public bool IsProductAvailable(int quantityToCheck)
        {
            return QuantityInStock >= quantityToCheck;
        }

        public decimal GetInventoryValue()
        {
            return Product.Price * QuantityInStock;
        }

        public bool IsLowStock(int threshold)
        {
            return QuantityInStock < threshold;
        }

        public bool IsOutOfStock()
        {
            return QuantityInStock == 0;
        }

    }
}
