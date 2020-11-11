using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TaskProgramming
{
    public class LockRecursion
    {
         static SpinLock sl = new SpinLock(true);
        //true mean specifying the spinlock knows which thread is currently locked.
        // if spin lock doesn't know which thread is lock 
        //, it will lead to dead lock which means the current lock is not released,
        //and recursive function will lead the spinlock go again the thread which is currently locked.
        //and Spinlock doesn't support lock recursion.
        public static void LockRecursionExample(int x)
        {
            bool lockTaken = false;
            try
            {
                sl.Enter(ref lockTaken);
            }
            catch (LockRecursionException e)
            {
                Console.WriteLine("Exception" + e);
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"Took a lock, x = {x}");
                    LockRecursionExample(x - 1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"Failed to take a lock, x = {x}");
                }
            }
        }
        public  void Main()
        {
            LockRecursionExample(5);

        }
    }
}
