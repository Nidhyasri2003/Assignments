using BankingSystem.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.dao
{
    public interface IBankService : ICustomerService
    {
        void CreateAccount(Customer customer, string accountType, decimal balance);
        List<Account> ListAccounts();
        Account GetAccountDetails(int accountId);
    }
}