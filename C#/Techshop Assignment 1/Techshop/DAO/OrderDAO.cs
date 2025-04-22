using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techshop.Entities;
using Techshop.Exceptions;
using Techshop.Util;

namespace Techshop.DAO
{
    public class OrderDAO : IOrderDAO
    {
        private readonly string connStr;

        public OrderDAO()
        {
            connStr = DBPropertyUtil.GetConnectionString("TechShopDB");
        }

        public void PlaceOrder(Order order, List<OrderDetail> orderDetails)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Insert into Orders
                    string orderQuery = "INSERT INTO Orders (CustomerID, OrderDate, TotalAmount) " +
                                        "OUTPUT INSERTED.OrderID " +
                                        "VALUES (@CustomerID, @OrderDate, @TotalAmount)";
                    SqlCommand orderCmd = new SqlCommand(orderQuery, conn, transaction);
                    orderCmd.Parameters.AddWithValue("@CustomerID", order.Customer.CustomerID);
                    orderCmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    orderCmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                    int orderId = (int)orderCmd.ExecuteScalar();

                    // Insert into OrderDetails
                    foreach (var detail in orderDetails)
                    {
                        // Step 1: Check stock from Inventory table
                        string checkStockQuery = "SELECT QuantityInStock FROM Inventory WHERE ProductID = @ProductID";
                        SqlCommand stockCmd = new SqlCommand(checkStockQuery, conn, transaction);
                        stockCmd.Parameters.AddWithValue("@ProductID", detail.Product.ProductID);
                        object stockResult = stockCmd.ExecuteScalar();

                        int stock = stockResult != null ? Convert.ToInt32(stockResult) : 0;

                        if (stock < detail.Quantity)
                        {
                            throw new InsufficientStockException($"Not enough stock for Product ID {detail.Product.ProductID}. Only {stock} available.");
                        }

                        // Step 2: Add to OrderDetails
                        string detailQuery = "INSERT INTO OrderDetails (OrderID, ProductID, Quantity) " +
                                             "VALUES (@OrderID, @ProductID, @Quantity)";
                        SqlCommand detailCmd = new SqlCommand(detailQuery, conn, transaction);
                        detailCmd.Parameters.AddWithValue("@OrderID", orderId);
                        detailCmd.Parameters.AddWithValue("@ProductID", detail.Product.ProductID);
                        detailCmd.Parameters.AddWithValue("@Quantity", detail.Quantity);
                        detailCmd.ExecuteNonQuery();

                        // Step 3: Reduce stock
                        string updateStockQuery = "UPDATE Inventory SET QuantityInStock = QuantityInStock - @Qty WHERE ProductID = @ProductID";
                        SqlCommand updateStockCmd = new SqlCommand(updateStockQuery, conn, transaction);
                        updateStockCmd.Parameters.AddWithValue("@Qty", detail.Quantity);
                        updateStockCmd.Parameters.AddWithValue("@ProductID", detail.Product.ProductID);
                        updateStockCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    Console.WriteLine("Order placed successfully!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Failed to place order: {ex.Message}");
                }
            }
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "SELECT * FROM Orders";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    orders.Add(new Order
                    {
                        OrderID = reader.GetInt32(0),
                        Customer = new Customer { CustomerID = reader.GetInt32(1) },
                        OrderDate = reader.GetDateTime(2),
                        TotalAmount = reader.GetDecimal(3)
                    });
                }
            }

            return orders;
        }

        public Order GetOrderById(int orderId)
        {
            Order order = null;

            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "SELECT * FROM Orders WHERE OrderID = @OrderID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    order = new Order
                    {
                        OrderID = reader.GetInt32(0),
                        Customer = new Customer { CustomerID = reader.GetInt32(1) },
                        OrderDate = reader.GetDateTime(2),
                        TotalAmount = reader.GetDecimal(3)
                    };
                }
            }

            return order;
        }

        public void CancelOrder(int orderId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "DELETE FROM OrderDetails WHERE OrderID = @OrderID; DELETE FROM Orders WHERE OrderID = @OrderID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Order cancelled successfully!");
            }
        }

        public void UpdateOrderStatus(int orderId, string newStatus)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "UPDATE Orders SET Status = @Status WHERE OrderID = @OrderID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Status", newStatus);
                cmd.Parameters.AddWithValue("@OrderID", orderId);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Order status updated.");
            }
        }
    }
}