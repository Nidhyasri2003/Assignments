using System;
using System.Data.SqlClient;
using BankingSystem.util;
using BankingSystem.exception;

namespace BankingSystem.dao
{
    public class CustomerServiceImpl : ICustomerService
    {
        public decimal GetBalance(int accountId)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                string query = "SELECT balance FROM Accounts WHERE account_id = @accId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@accId", accountId);

                object result = cmd.ExecuteScalar();
                if (result != null)
                    return Convert.ToDecimal(result);
                else
                    throw new InvalidAccountException("Account ID not found.");
            }
        }

        public decimal Deposit(int accountId, decimal amount)
        {
            decimal currentBalance = GetBalance(accountId);
            decimal newBalance = currentBalance + amount;

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                string update = "UPDATE Accounts SET balance = @balance WHERE account_id = @accId";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@balance", newBalance);
                cmd.Parameters.AddWithValue("@accId", accountId);
                cmd.ExecuteNonQuery();
            }

            return newBalance;
        }

        public decimal Withdraw(int accountId, decimal amount)
        {
            decimal currentBalance = GetBalance(accountId);

            if (amount > currentBalance)
                throw new InsufficientFundsException("Insufficient funds.");

            decimal newBalance = currentBalance - amount;

            using (SqlConnection conn = DBUtil.GetConnection())
            {
                conn.Open();
                string update = "UPDATE Accounts SET balance = @balance WHERE account_id = @accId";
                SqlCommand cmd = new SqlCommand(update, conn);
                cmd.Parameters.AddWithValue("@balance", newBalance);
                cmd.Parameters.AddWithValue("@accId", accountId);
                cmd.ExecuteNonQuery();
            }

            return newBalance;
        }

        public void Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            Withdraw(fromAccountId, amount);
            Deposit(toAccountId, amount);
        }
    }
}
