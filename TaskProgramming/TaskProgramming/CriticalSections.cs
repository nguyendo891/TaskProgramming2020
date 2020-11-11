using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class CriticalSections
    {
        internal class BankAccount
        {
            public object padlock = new object();
            public int Balance { get; private set; }
            public void Deposit(int amount)
            {
                //+=
                // op1 : temp <- get_Balance() + amount
                // op2 : set_Balance(temp)
                lock (padlock)
                {
                    Balance += amount;
                }
            }

            public void Withdraw(int amount)
            {
                lock (padlock)
                {
                    Balance -= amount;
                }

            }
        }
        public void Main()
        {
            var tasks = new List<Task>();

            var ba = new BankAccount();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    ba.Deposit(100);
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    ba.Withdraw(100);
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}.");
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
