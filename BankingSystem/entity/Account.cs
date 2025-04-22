using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.entity
{
    public class Account
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
    }
}