using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techshop.Entities;

namespace Techshop.DAO
{
    public interface IProductDAO
    {
        void AddProduct(Product product);
        List<Product> GetAllProducts();
        Product GetProductById(int productId);
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);
    }
}