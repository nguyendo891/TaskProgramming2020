using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class InterlockedOperation
    {
        internal class BankAccount
        {
            private int balance;

            public int Balance { get => balance; private set => balance = value; }
            public void Deposit(int amount)
            {
                //+=
                // op1 : temp <- get_Balance() + amount
                // op2 : set_Balance(temp)
                Interlocked.Add(ref balance, amount);
                //1
                //2
                //Thread.MemoryBarrier();
                //3
            }

            public void Withdraw(int amount)
            {
                //lock free programming
                Interlocked.Add(ref balance, -amount);

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
