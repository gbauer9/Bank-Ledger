using System;
using System.Text.RegularExpressions;
using System.IO;

namespace BankLedger
{
    // Class to help do misc. tasks and shorten other parts of application
    static class BankLedgerHelper
    {
        // Long strings used to print the menus and prompts
        public const string loginMenuMessage = "*************************\nWelcome, please sign in or make a new account\n1. Log In\n2. Create Account\n3. Exit Program\n*************************\n-> ";
        
        public const string accountMenuMessage = "*************************\nWhat would you like to do?\n1. See Balance\n2. Make Withdrawal\n3. Make Deposit\n4. See Transaction History\n5. Log Out\n6. Exit Program\n*************************\n-> ";

        public const string usernameMessage = "*************************\nPlease enter your username\n*************************\n-> ";
        
        public const string passwordMessage = "*************************\nPlease enter your password\n*************************\n-> ";
        
        public const string noUsernameMessage = "*************************\nThat username does not exist in the system, please try again\n*************************\n";
        
        public const string wrongPasswordMessage = "*************************\nThat password does not match the username, please try again\n*************************\n";

        public const string transactionMessage = "*************************\nEnter the transaction amount\n*************************\n-> ";

        public const string usernameTakenMessage = "*************************\nThat username is tied to another account, please try a different one\n*************************\n";

        public const string overdrawMessage = "*************************\nThat transaction was not made as it would result in an overdraw, please try again\n*************************";

        public const string overflowMessage = "*************************\nThat transaction was not made as it would result in a decimal overflow, please try again\n*************************";
        
        public const string invalidTransactionMessage = "*************************\nYou entered an invalid number, please enter a positive integer or decimal value rounded to the nearest hundredth\nTransactions are limited to 6 figures\n*************************\n-> ";

        public const string invalidPassword = "*************************\nPassword must be between 8 and 16 characters.\n*************************\n-> ";

        // Prints login menu to stdout and recieves user's response
        public static string DisplayLoginMenu()
        {
            Console.Write(loginMenuMessage);

            return Console.ReadLine(); 
        }

        // Prints account menu to stdout and recieve's user's response
        public static string DisplayAccountMenu()
        {
            Console.Write(accountMenuMessage);

            return Console.ReadLine(); 
        }

        // Prints prompt to stdout and recieve's user's response
        public static string GetUsername()
        {
            Console.Write(usernameMessage);
            
            return Console.ReadLine();
        }

        // Prints prompt to stdout and recieve's user's response
        public static string GetPassword()
        {
            Console.Write(passwordMessage);
            string response = Console.ReadLine();

            while(response.Length > 16 | response.Length < 8)
            {
                Console.Write(invalidPassword);
                response = Console.ReadLine();
            }

            return response;
        }

        // Create a new password, similar to GetPassword but checks for string length to ensure better password (for theoretical security purposes)
        public static string CreatePassword()
        {
            Console.Write(passwordMessage);
            string response = Console.ReadLine();

            while(response.Length < 8)
            {
                Console.Write(invalidPassword);
                response = Console.ReadLine();
            }

            return response;
        }

        // Get user input on the value of the transaction (withdraw or deposit) transactions are in the form of an integer
        // or decimal rounded to the nearest hundredth. They are limited to 6 figures to avoid decimal overflow when converting
        // from string to decimal
        public static decimal GetTransactionAmount()
        {
            Console.Write(transactionMessage);
            string input = Console.ReadLine();

            while(!Regex.IsMatch(input, @"^\d{1,6}(.\d{0,2}){0,1}$"))
            {   
                Console.Write(invalidTransactionMessage);
                input = Console.ReadLine();
                Console.Clear();
            }

            return Decimal.Parse(input);
        }

        // Removes all whitespace from a string
        public static string TrimWhitespace(string input) => Regex.Replace(input, @"\s", string.Empty);
    }
}