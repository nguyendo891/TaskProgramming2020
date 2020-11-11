using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TaskProgramming
{
   
    class MutexProgramming
    {
        internal class BankAccount
        {
            private int balance;
            public int Balance { get { return balance; } private set { balance = value; } }
            public void Deposit(int amount)
            {
                //+=
                // op1 : temp <- get_Balance() + amount
                // op2 : set_Balance(temp)

                Balance += amount;

            }

            public void Withdraw(int amount)
            {

                Balance -= amount;

            }

            public void Transfer(BankAccount where, int amount)
            {
                Balance -= amount;
                where.Balance += amount;
            }
        }
        public  void Main()
        {
            //var tasks = new List<Task>();

            //var ba = new BankAccount();
            //var ba2 = new BankAccount();

            //Mutex mutex = new Mutex();
            //Mutex mutex2 = new Mutex();
            //for (int i = 0; i < 10; i++)
            //{
            //    tasks.Add(Task.Factory.StartNew(() =>
            //    {
            //        for (int i = 0; i < 1000; i++)
            //        {
            //            bool haveLock = mutex.WaitOne();
            //            try
            //            {
            //                ba.Deposit(1);
            //            }
            //            finally
            //            {
            //                if (haveLock) mutex.ReleaseMutex();
            //            }

            //        }

            //    }));

            //    tasks.Add(Task.Factory.StartNew(() =>
            //    {
            //        for (int i = 0; i < 1000; i++)
            //        {

            //            bool haveLock = mutex2.WaitOne();
            //            try
            //            {
            //                ba2.Deposit(1);
            //            }
            //            finally
            //            {
            //                if (haveLock) mutex2.ReleaseMutex();
            //            }
            //        }

            //    }));

            //    tasks.Add(Task.Factory.StartNew(() =>
            //    {
            //        for (int j = 0; j < 1000; j++)
            //        {
            //            bool haveLock = WaitHandle.WaitAll(new[] { mutex, mutex2 });
            //            try
            //            {
            //                ba.Transfer(ba2, 1);
            //            }
            //           finally
            //            {
            //                if(haveLock)
            //                {
            //                    mutex.ReleaseMutex();
            //                    mutex2.ReleaseMutex();
            //                }
            //            }
            //        }
            //    }));
            //}

            //Task.WaitAll(tasks.ToArray());
            //Console.WriteLine($"Final balance ba is {ba.Balance}.");
            //Console.WriteLine($"Final balance ba2 is {ba2.Balance}.");
            //Console.WriteLine("Hello World!");
            //Console.ReadKey();
            const string appName = "TaskProgramming";
            Mutex mutex;
            try
            {
                mutex = Mutex.OpenExisting(appName);
                Console.WriteLine($"Sorry, {appName} is already running");
            }
            catch (WaitHandleCannotBeOpenedException e)
            {
                Console.WriteLine("We can run the program just fine");
                mutex = new Mutex(false, appName);
            }

            Console.ReadKey();
            mutex.ReleaseMutex();
        }
    }
}
