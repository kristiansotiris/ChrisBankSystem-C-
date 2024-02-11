using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    class Transition
    {
        public string UserName { get; set; }
        public long AccountNumber { get; set; } // Changed to long
        public decimal Amount { get; set; }
        public DateTime TransactionTime { get; set; }
        public TransactionType Type { get; set; }

        public enum TransactionType
        {
            Deposit,
            Withdraw
        }

        static List<Tuple<string, long, decimal>> accounts = new List<Tuple<string, long, decimal>>();
        static List<Transition> transactions = new List<Transition>();

        static void DepositMoney(Tuple<string, long, decimal> userAccount) // Changed to long
        {
            decimal amount;

            do
            {
                Console.Write("Please Enter The Amount That You Want to Deposit:");

                if (decimal.TryParse(Console.ReadLine(), out amount))
                {
                    // Update account balance
                    UpdateAccountBalance(userAccount.Item2, amount);

                    // Log the transaction
                    LogTransaction(userAccount, amount, TransactionType.Deposit);

                    Console.WriteLine();
                    Console.WriteLine($"Successfully deposited {amount}$ into your account\n");

                    // Display account balance
                    Console.WriteLine($"Current Balance: {GetAccountBalance(userAccount.Item2)}$\n");
                    break; // Exit the loop after successful deposit
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.\n");
                }

            } while (true);
        }

        static void WithdrawMoney(Tuple<string, long, decimal> userAccount) // Changed to long
        {
            do
            {
                Console.WriteLine("Enter the amount to withdraw:");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    // Retrieve the current account balance
                    decimal currentBalance = GetAccountBalance(userAccount.Item2);

                    // Check if the account has sufficient balance
                    if (currentBalance >= amount)
                    {
                        // Update account balance by deducting the withdrawal amount
                        UpdateAccountBalance(userAccount.Item2, -amount);

                        // Log the transaction
                        LogTransaction(userAccount, amount, TransactionType.Withdraw);

                        Console.WriteLine($"Successfully withdrawn {amount}$ from your account.");

                        // Display updated account balance
                        Console.WriteLine($"Current Balance: {GetAccountBalance(userAccount.Item2)}$\n");

                        break; // Exit the loop after successful withdrawal
                    }
                    else
                    {
                        Console.WriteLine("Insufficient balance in your account.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid amount.");
                }

            } while (true);
        }

        static decimal GetAccountBalance(long accountNumber) // Changed to long
        {
            foreach (var account in accounts)
            {
                if (account.Item2 == accountNumber)
                {
                    return account.Item3;
                }
            }
            return 0; // Return 0 if account not found
        }

        static void UpdateAccountBalance(long accountNumber, decimal amount) // Changed to long
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].Item2 == accountNumber)
                {
                    accounts[i] = new Tuple<string, long, decimal>(accounts[i].Item1, accounts[i].Item2, accounts[i].Item3 + amount);
                    return;
                }
            }
        }

        static void LogTransaction(Tuple<string, long, decimal> userAccount, decimal amount, TransactionType type) // Changed to long
        {
            // Log the transaction details
            Transition transaction = new Transition
            {
                UserName = userAccount.Item1,
                AccountNumber = userAccount.Item2,
                Amount = amount,
                TransactionTime = DateTime.Now,
                Type = type
            };

            transactions.Add(transaction);
        }

        static void Profile(Tuple<string, long, decimal> userAccount) // Changed to long
        {
            int choice;
            string userName = userAccount.Item1;
            Console.WriteLine($"Welcome Back {userName}\n");

            do
            {
                Console.WriteLine("1. Deposit Money");
                Console.WriteLine("2. Withdraw Money");
                Console.WriteLine("3. View History");
                Console.WriteLine("4. Home Menu\n");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            DepositMoney(userAccount);
                            Thread.Sleep(1000);
                            break;

                        case 2:
                            WithdrawMoney(userAccount);
                            Thread.Sleep(1000);
                            break;

                        case 3:
                            ViewHistory(userAccount);
                            Thread.Sleep(1000);
                            break;

                        case 4:
                            StartingMenu();
                            Thread.Sleep(1000);
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please select a valid option.\n");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number corresponding to your choice.\n");
                }

            } while (true);
        }

        static void ViewHistory(Tuple<string, long, decimal> userAccount) // Changed to long
        {
            Thread.Sleep(1000);

            Console.WriteLine($"Transaction history for account number {userAccount.Item2}:");
            foreach (var transaction in transactions)
            {
                if (transaction.AccountNumber == userAccount.Item2)
                {
                    Console.WriteLine($"Transaction Type: {transaction.Type}, Amount: {transaction.Amount}$, Time: {transaction.TransactionTime}");
                }
            }

            Profile(userAccount);
        }

        static void StartingMenu()
        {
            Console.WriteLine("Welcome To Chris BANK SYSTEM");
            Console.WriteLine();
            int choice;
            do
            {
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Enter Account");
                Console.WriteLine("3. Display Accounts");
                Console.WriteLine("4. Exit Program\n");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            CreatAccount();
                            Thread.Sleep(1000);
                            return;

                        case 2:
                            LogIn();
                            Thread.Sleep(1000);
                            return;

                        case 3:
                            DisplayAccounts();
                            Thread.Sleep(1000);
                            return;

                        case 4:
                            Console.WriteLine("Program Will Shut Down !\n");
                            Console.WriteLine("Please Press Enter To SHUT DOWN !");
                            Thread.Sleep(1000);
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine();
                            Console.WriteLine("Please Make Sure You Choose The Right Option ");
                            Console.WriteLine();
                            continue;
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Please Enter A Right Type of Input: \n");
                }
            } while (choice != 4);
        }

        static void CreatAccount()
        {
            do
            {
                Console.WriteLine();
                Console.Write("Please Enter Your Name: ");
                string userName = Console.ReadLine();

                if (userName.Length > 20)
                {
                    Console.WriteLine();
                    Console.WriteLine("Too Long Name Please Try Again");
                }

                else if (ContainesNumbers(userName))
                {
                    Console.WriteLine();
                    Console.WriteLine("Username Cannot Contain Numeric Characters. Please try again");
                    continue; // Restart the loop to prompt the user for a valid username
                }

                else if (string.IsNullOrEmpty(userName))
                {
                    Console.WriteLine();
                    return;
                }


                //ACOUNT NUMBER METHOD
                int minAccountRange = 1000;
                int maxAccountRange = 9999;

                Console.Write("Please Enter Your Code: ");

                if (!long.TryParse(Console.ReadLine(), out long accountNumber))
                {
                    Console.WriteLine();
                    Console.WriteLine("Please Make Sure You Write Type Of Numbers:\n");
                    return;
                }

                else if (accountNumber < minAccountRange || accountNumber > 9999)
                {
                    Console.WriteLine($"Account Number Should Be At Range{minAccountRange} & {maxAccountRange}");
                }

                Tuple<string, long, decimal> account = new Tuple<string, long, decimal>(userName, accountNumber, 0);
                accounts.Add(account);

                Console.WriteLine();
                Console.WriteLine("Your Account Has Been Successfully Created\n");
                Console.WriteLine();
                Console.WriteLine($"Name: {userName}");
                Console.WriteLine($"AccountCode: {accountNumber}\n");
                Console.WriteLine();
                StartingMenu();
                return;

            } while (true);
        }

        //contains numbers method 
        static bool ContainesNumbers(string input)
        {
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

        static void DisplayAccounts()
        {
            string correctPasswordInput = "manager123";
            int attempts = 0;
            int maxAttempts = 3;

            Console.WriteLine();

            if (accounts.Count == 0)
            {
                Console.WriteLine("No Accounts Available\n");
                Console.WriteLine();
                StartingMenu();
                return;
            }

            do
            {
                Console.WriteLine("Please Enter The Password");
                string passwordInput = Console.ReadLine();

                if (string.IsNullOrEmpty(passwordInput))
                {
                    Console.WriteLine("Enter The Passowrd");
                }
                else if (passwordInput == correctPasswordInput)
                {
                    Console.WriteLine("\nAccounts:\n");
                    foreach (var account in accounts)
                    {
                        Console.WriteLine($"Name: {account.Item1}, Account Number: {account.Item2}, Balance: {account.Item3}$");
                        Console.WriteLine();
                        StartingMenu();
                        return;
                    }
                }
                else
                {
                    attempts++;
                    if (attempts > maxAttempts)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Too Many Attempts Returning Back\n");
                        StartingMenu();
                        return;
                    }
                    Console.WriteLine();
                    Console.WriteLine($"Incorrect Password Please Try Again" +
                                      $"Attempts Left : {maxAttempts - attempts}");
                    Console.WriteLine();
                }
            } while (true);
        }

        static void LogIn()
        {
            int attempts = 0;
            int maxAttempts = 3;
            long accountNumber;
            Thread.Sleep(1000);

            do
            {
                Console.WriteLine("Please Enter Your Account Number to Log In: ");
                StringBuilder accountNumberBuilder = new StringBuilder();
                Console.WriteLine();
                Console.WriteLine();

                ConsoleKeyInfo key;
                while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
                {
                    if (char.IsDigit(key.KeyChar))
                    {
                        accountNumberBuilder.Append(key.KeyChar);
                        Console.Write("*");
                    }
                    else if (key.Key == ConsoleKey.Backspace && accountNumberBuilder.Length > 0)
                    {
                        accountNumberBuilder.Remove(accountNumberBuilder.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                }

                Console.WriteLine();

                if (!long.TryParse(accountNumberBuilder.ToString(), out accountNumber))
                {
                    Console.WriteLine("Invalid input. Please enter a valid account number.");
                    Console.WriteLine();
                    continue;
                }

                foreach (var account in accounts)
                {
                    if (account.Item2 == accountNumber)
                    {
                        Console.WriteLine("Login successful!");
                        Console.WriteLine();

                        Profile(account);
                        return;
                    }
                }

                attempts++;

                if (attempts >= maxAttempts)
                {
                    Console.WriteLine("Maximum login attempts reached. Returning back to menu.");
                    StartingMenu();
                    return;
                }
                else
                {
                    Console.WriteLine($"Incorrect account number. Attempts left: {maxAttempts - attempts}");
                    Console.WriteLine();
                }

            } while (attempts < maxAttempts);
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            StartingMenu();
            Console.ReadKey();
        }
    }
}