using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BankingSystem.entity;
using BankingSystem.exception;
using BankingSystem.util;

namespace BankingSystem.dao
{
    public class BankServiceImpl : CustomerServiceImpl, IBankService
    {
        public void CreateAccount(Customer customer, string accountType, decimal balance)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();

                // 1. Insert into Customers
                string insertCustomer = @"INSERT INTO Customers 
        (first_name, last_name, DOB, email, phone_number, address)
        VALUES (@firstName, @lastName, @dob, @email, @phone, @address);
        SELECT SCOPE_IDENTITY();";

                SqlCommand custCmd = new SqlCommand(insertCustomer, conn);
                custCmd.Parameters.AddWithValue("@firstName", customer.FirstName);
                custCmd.Parameters.AddWithValue("@lastName", customer.LastName);
                custCmd.Parameters.AddWithValue("@dob", customer.DOB);
                custCmd.Parameters.AddWithValue("@email", customer.Email);
                custCmd.Parameters.AddWithValue("@phone", customer.PhoneNumber);
                custCmd.Parameters.AddWithValue("@address", customer.Address);

                int customerId = Convert.ToInt32(custCmd.ExecuteScalar());

                // 2. Insert into Accounts
                string insertAccount = @"INSERT INTO Accounts 
        (customer_id, account_type, balance) 
        VALUES (@custId, @type, @balance)";

                SqlCommand accCmd = new SqlCommand(insertAccount, conn);
                accCmd.Parameters.AddWithValue("@custId", customerId);
                accCmd.Parameters.AddWithValue("@type", accountType);
                accCmd.Parameters.AddWithValue("@balance", balance);
                accCmd.ExecuteNonQuery();

                Console.WriteLine("✅ Account created successfully!");
            }
        }
        public List<Account> ListAccounts()
        {
            List<Account> accounts = new List<Account>();

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Accounts";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Account acc = new Account
                    {
                        AccountId = Convert.ToInt32(reader["account_id"]),
                        CustomerId = Convert.ToInt32(reader["customer_id"]),
                        AccountType = reader["account_type"].ToString(),
                        Balance = Convert.ToDecimal(reader["balance"])
                    };
                    accounts.Add(acc);
                }
            }

            return accounts;
        }

        public Account GetAccountDetails(int accountId)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Accounts WHERE account_id = @accId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@accId", accountId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Account
                    {
                        AccountId = Convert.ToInt32(reader["account_id"]),
                        CustomerId = Convert.ToInt32(reader["customer_id"]),
                        AccountType = reader["account_type"].ToString(),
                        Balance = Convert.ToDecimal(reader["balance"])
                    };
                }
                else
                {
                    // ✅ Handle not found case
                    throw new InvalidAccountException($"Account with ID {accountId} not found.");
                }
            }
        }


        

        void IBankService.CreateAccount(Customer customer, string accountType, decimal balance)
        {
            throw new NotImplementedException();
        }

        List<Account> IBankService.ListAccounts()
        {
            throw new NotImplementedException();
        }

        Account IBankService.GetAccountDetails(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}
