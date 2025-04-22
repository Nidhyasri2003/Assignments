using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techshop.Entities;
using Techshop.Util;

namespace Techshop.DAO
{
    public class CustomerDAO : ICustomerDAO
    {
        private readonly string connStr;

        public CustomerDAO()
        {
            connStr = DBPropertyUtil.GetConnectionString("TechShopDB");
        }

        public void AddCustomer(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Email))
            {
                throw new InvalidDataException("Customer email cannot be empty.");
            }
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) " +
                               "VALUES (@FirstName, @LastName, @Email, @Phone, @Address)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd.Parameters.AddWithValue("@Address", customer.Address);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Customer added successfully!");
            }
        }

        public void DeleteCustomer(int customerId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "DELETE FROM Customers WHERE CustomerID = @CustomerID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerID", customerId);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Customer deleted successfully!");
            }
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "SELECT * FROM Customers";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Customer customer = new Customer()
                    {
                        CustomerID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = reader.GetString(3),
                        Phone = reader.GetString(4),
                        Address = reader.GetString(5)
                    };
                    customers.Add(customer);
                }
            }

            return customers;
        }

        public Customer GetCustomerById(int customerId)
        {
            Customer customer = null;

            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "SELECT * FROM Customers WHERE CustomerID = @CustomerID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerID", customerId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customer = new Customer()
                    {
                        CustomerID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = reader.GetString(3),
                        Phone = reader.GetString(4),
                        Address = reader.GetString(5)
                    };
                }
            }

            return customer;
        }

        public void UpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                string query = "UPDATE Customers SET FirstName = @FirstName, LastName = @LastName, " +
                               "Email = @Email, Phone = @Phone, Address = @Address WHERE CustomerID = @CustomerID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Customer updated successfully!");
            }
        }
    }
}


