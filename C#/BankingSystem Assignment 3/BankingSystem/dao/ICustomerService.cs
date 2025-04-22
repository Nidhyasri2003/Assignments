using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.dao
{
    public interface ICustomerService
    {
        decimal GetBalance(int accountId);
        decimal Deposit(int accountId, decimal amount);
        decimal Withdraw(int accountId, decimal amount);
        void Transfer(int fromAccountId, int toAccountId, decimal amount);
    }
}