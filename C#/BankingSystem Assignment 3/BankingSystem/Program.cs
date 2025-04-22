using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BankingSystem.dao;
using BankingSystem.entity;

namespace BankingSystem.main
{
    class BankApp
    {
        static void Main(string[] args)
        {
            BankServiceImpl bank = new BankServiceImpl();
            while (true)
            {
                Console.WriteLine("\n====== HM BANK SYSTEM ======");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Get Balance");
                Console.WriteLine("6. List Accounts");
                Console.WriteLine("7. Exit");
                Console.Write("Enter choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Customer c = new Customer();
                            Console.Write("First Name: "); c.FirstName = Console.ReadLine();
                            Console.Write("Last Name: "); c.LastName = Console.ReadLine();
                            Console.Write("DOB (yyyy-mm-dd): "); c.DOB = DateTime.Parse(Console.ReadLine());
                            Console.Write("Email: "); c.Email = Console.ReadLine();
                            Console.Write("Phone: "); c.PhoneNumber = Console.ReadLine();
                            Console.Write("Address: "); c.Address = Console.ReadLine();
                            Console.Write("Account Type (savings/current/zero_balance): ");
                            string type = Console.ReadLine();
                            Console.Write("Initial Balance: ");
                            decimal bal = Convert.ToDecimal(Console.ReadLine());

                            bank.CreateAccount(c, type, bal);
                            break;

                        case 2:
                            Console.Write("Enter Account ID: ");
                            int accId1 = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Amount to Deposit: ");
                            decimal amount1 = Convert.ToDecimal(Console.ReadLine());
                            var bal1 = bank.Deposit(accId1, amount1);
                            Console.WriteLine($"Updated Balance: {bal1}");
                            break;

                        case 3:
                            Console.Write("Enter Account ID: ");
                            int accId2 = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Amount to Withdraw: ");
                            decimal amount2 = Convert.ToDecimal(Console.ReadLine());
                            var bal2 = bank.Withdraw(accId2, amount2);
                            Console.WriteLine($"Updated Balance: {bal2}");
                            break;

                        case 4:
                            Console.Write("From Account ID: ");
                            int from = Convert.ToInt32(Console.ReadLine());
                            Console.Write("To Account ID: ");
                            int to = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Amount to Transfer: ");
                            decimal amt = Convert.ToDecimal(Console.ReadLine());
                            bank.Transfer(from, to, amt);
                            Console.WriteLine("Transfer successful.");
                            break;

                        case 5:
                            Console.Write("Enter Account ID: ");
                            int accId = Convert.ToInt32(Console.ReadLine());
                            decimal bal3 = bank.GetBalance(accId);
                            Console.WriteLine($"Current Balance: {bal3}");
                            break;

                        case 6:
                            var accList = bank.ListAccounts();
                            foreach (var a in accList)
                                Console.WriteLine($"{a.AccountId} - {a.AccountType} - ₹{a.Balance}");
                            break;

                        case 7:
                            return;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}