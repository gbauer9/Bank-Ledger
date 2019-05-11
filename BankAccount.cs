using System;
using System.Collections.Generic;

namespace BankLedger
{
    class  BankAccount
    {
        // Starting balance used for calculating the current balance later on
        private decimal startingBalance;

        // Balance is calculated using past transactions so there is no possibility of difference between balance and transaction history
        public decimal balance 
        { 
            get
            {
                decimal balance = this.startingBalance;
                foreach (var trans in pastTransactions)
                {
                    balance += trans.amount;
                }

                return Decimal.Round(balance, 2);
            }
        }
        
        // Account owner's username ties user to their account
        public string accountOwner { get; }

        // Password to access the account
        // Passwords are stored in plaintext for simplicity's sake but of course if this were a production-level application they would be encrypted
        // on secure hardware
        public string accountPassword { get; }

        // List of all previous transactions which includes their amounts and time
        private List<Transaction> pastTransactions = new List<Transaction>();

        // BankAccount class constructor
        public BankAccount(string owner, string password, decimal startingBalance)
        {
            this.accountOwner = owner;
            this.startingBalance = Decimal.Round(startingBalance, 2);
            this.accountPassword = password;
        }

        // Create withdrawal transaction and return true if it would not result in an overdraw, else return false
        public bool Withdraw(decimal amount, DateTime time)
        {
            if(balance - amount < 0)
            {
                return false;
            }
            Transaction withdrawal = new Transaction(amount * -1, time, "Withdraw");
            pastTransactions.Add(withdrawal);
            return true;
        }   

        // Create deposit transaction and return true if it doesn't result in integer overflow, else return false
        public bool Deposit(decimal amount, DateTime time)
        {
            try
            {
                decimal test = checked(balance + amount);
            }
            catch (OverflowException)
            {
                return false;
            }
            Transaction deposit = new Transaction(amount, time, "Deposit");
            pastTransactions.Add(deposit);
            return true;
        }     

        // Prints the transaction history in a neat format to stdout. No return value
        public void PrintTransactionHistory()
        {
            decimal runningBalance = startingBalance;
            Console.WriteLine("*************************\nType\tAmount\tDate\tBalance");
            foreach(var trans in pastTransactions)
            {
                runningBalance += trans.amount;
                Console.WriteLine($"{trans.type}\t{trans.amount}\t{trans.time}\t{runningBalance}");
            }
            Console.WriteLine("*************************");
            return;
        }
    }
}