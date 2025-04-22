using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techshop.Entities;
using Techshop.Util;

namespace Techshop.DAO
{
    public class ProductDAO : IProductDAO
    {
        private readonly string connStr;

        public ProductDAO()
        {
            connStr = DBPropertyUtil.GetConnectionString("TechShopDB");
        }

        public void AddProduct(Product product)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "INSERT INTO Products (ProductName, Description, Price) VALUES (@ProductName, @Description, @Price)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Product added successfully!");
            }
        }

        public void DeleteProduct(int productId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductID", productId);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Product deleted successfully!");
            }
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "SELECT * FROM Products";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Description = reader.GetString(2),
                        Price = reader.GetDecimal(3)
                    };
                    products.Add(product);
                }
            }

            return products;
        }

        public Product GetProductById(int productId)
        {
            Product product = null;

            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "SELECT * FROM Products WHERE ProductID = @ProductID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductID", productId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Description = reader.GetString(2),
                        Price = reader.GetDecimal(3)
                    };
                }
            }

            return product;
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "UPDATE Products SET ProductName = @ProductName, Description = @Description, Price = @Price WHERE ProductID = @ProductID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Product updated successfully!");
            }
        }
    }
}
