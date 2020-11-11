using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class SpinLockProgramming
    {
        public class BankAcc
        {
            private int balance;

            public int Balance { get => balance; private set => balance = value; }
            public void Deposit(int amount)
            {
                //+=
                // op1 : temp <- get_Balance() + amount
                // op2 : set_Balance(temp)
                balance += amount;
                //1
                //2
                //Thread.MemoryBarrier();
                //3
            }

            public void Withdraw(int amount)
            {
                //lock free programming
                balance -= amount;

            }

            public void Main()
            {
                var tasks = new List<Task>();

                var ba = new BankAcc();

                SpinLock sl = new SpinLock();

                for (int i = 0; i < 10; i++)
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        bool lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Deposit(100);
                        }
                        finally
                        {
                            if (lockTaken) sl.Exit();
                        }

                    }));

                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        bool lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Withdraw(100);
                        }
                        finally
                        {
                            if (lockTaken) sl.Exit();
                        }
                    }));
                }

                Task.WaitAll(tasks.ToArray());
                Console.WriteLine($"Final balance is {ba.Balance}.");
                Console.WriteLine("Hello World!");
                Console.ReadKey();
            }
        }
    }
}
