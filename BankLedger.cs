using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;


namespace BankLedger
{
    static class BankProgram
    {

        // Holds all user accounts using the username as the key and BankAccount instance as value
        private static Dictionary<string, BankAccount> accounts = new Dictionary<string, BankAccount>();

        static void Main(string[] args)
        {
            Console.Clear();
            // Run main program loop
            RunLedger();
        }

        // Main program loop 
        // No parameters
        // On return the application closes
        static void RunLedger()
        {
            DemoSetup();
            // Used to see if the account menu should be displayed
            bool loggedIn = false;
            // Used to point to the current user's account instance
            BankAccount userAccount = null; 

            do{
                // Get the user's menu choice and trim out all whitespace
                string startMenuInput = BankLedgerHelper.DisplayLoginMenu();
                startMenuInput = BankLedgerHelper.TrimWhitespace(startMenuInput);
                Console.Clear();

                // Either log in or create an account
                switch(startMenuInput)
                {
                    // Get the username and check it with the dictionary of accounts, then get password and check that it matches with username
                    // If everything checks out, set loggedIn to true and continue to account menu
                    case "1":
                        // Get and confirm username
                        string username = BankLedgerHelper.GetUsername();
                        Console.Clear();
                        if(!DoesusernameExist(username.ToLower()))
                        {
                            Console.Write(BankLedgerHelper.noUsernameMessage);
                            break;
                        }
                        // Get and confirm password
                        string password = BankLedgerHelper.GetPassword();
                        Console.Clear();
                        if(!DoesPassMatch(username, password))
                        {
                            Console.Write(BankLedgerHelper.wrongPasswordMessage);
                            break;
                        }
                        // Set the user's account object to the corresponding 
                        userAccount = accounts[username];
                        loggedIn = true;
                        break;
                    // Create an account
                    case "2":
                        // Get username and check if it is already a key in accounts dictionary
                        username = BankLedgerHelper.GetUsername();
                        Console.Clear();
                        if(DoesusernameExist(username.ToLower()))
                        {
                            Console.Write(BankLedgerHelper.usernameTakenMessage);
                            break;
                        }
                        // Let the user enter a password and create a new account, add it to dictionary of accounts
                        // Starting balance set to $2000 by default for demo purposes
                        password = BankLedgerHelper.CreatePassword();
                        Console.Clear();
                        userAccount = new BankAccount(username, password, 2000.00m);
                        accounts.Add(userAccount.accountOwner.ToLower(), userAccount);
                        loggedIn = true;
                        break;
                    // Return out of main loop, therefore ending the application
                    case "3":
                        Console.WriteLine("Goodbye.");
                        return;
                    // The user's input was not a valid option, so tell them so and keep running the program
                    default:
                        Console.WriteLine("Not a valid option, please choose either 1, 2, or 3.");
                        break;
                }

                // Menu loop for when the user has successfully logged in to their account
                while(loggedIn)
                {
                    // Get input from user and trim all whitespace
                    string accountMenuInput = BankLedgerHelper.DisplayAccountMenu();
                    accountMenuInput = BankLedgerHelper.TrimWhitespace(accountMenuInput);
                    Console.Clear();

                    switch(accountMenuInput)
                    {
                        // Show the current balance
                        case "1":
                            Console.Write($"*************************\nYour current account balance is: ${userAccount.balance}\n*************************\n");
                            break;
                        case "2":
                            decimal withdrawAmount = BankLedgerHelper.GetTransactionAmount();
                            Console.Clear();
                            bool notOverdraw = userAccount.Withdraw(withdrawAmount, DateTime.Now);
                            if(!notOverdraw)
                            {
                                Console.WriteLine(BankLedgerHelper.overdrawMessage);
                                break;
                            }
                            Console.WriteLine($"*************************\nSuccess, your new balance is ${userAccount.balance}\n*************************");
                            break;
                        case "3":Console.Clear();
                            decimal depositAmount = BankLedgerHelper.GetTransactionAmount();
                            Console.Clear();
                            bool notOverflow = userAccount.Deposit(depositAmount, DateTime.Now);
                            if(!notOverflow)
                            {
                                Console.WriteLine(BankLedgerHelper.overflowMessage);
                                break;
                            }
                            Console.WriteLine($"*************************\nSuccess, your new balance is ${userAccount.balance}\n*************************");
                            break;
                        case "4":
                            userAccount.PrintTransactionHistory();
                            break;
                        // Change loggedIn to false to display login menu
                        case "5":
                            userAccount = null;
                            loggedIn = false;
                            break;
                        // Exit program
                        case "6":
                            Console.WriteLine("Goodbye.");
                            return;
                        default:
                            Console.WriteLine("Not a valid option, please choose 1, 2, 3, 4, 5, or 6.");
                            break;
                    }
                }
            }while(true);
        }

        // Set up some dummy accounts for demo purposes
        static void DemoSetup()
        {
            accounts.Add("altsource", new BankAccount("altsource", "password", 2000.00m));
            accounts.Add("bauerga", new BankAccount("bauerga", "password", 2000.00m));
            return;
        }

        // Checks if given username is in the dictionary of accounts
        // Parameters: username (string)
        // Returns true if username is in dictionary, false otherwise
        static bool DoesusernameExist(string username) => accounts.ContainsKey(username);

        // Checks if given password matches that in the account
        // Parameters: username (string), password (string)
        // Returns true if password matches that in the user's BankAccount object, false otherwise
        static bool DoesPassMatch(string username, string password) => accounts[username].accountPassword == password;
    }
}
