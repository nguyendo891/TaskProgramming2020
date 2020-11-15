using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class ThreadLocalStorage
    {
        static void ThreadLocalStorageMain(string[] args)
        {
            //int sum = 0;
            //Parallel.For(1, 1001, x =>
            //{
            //    Interlocked.Add(ref sum, x);
            //});

            int sum = 0;
            Parallel.For(1, 1001,
                () => 0,// () => is the local storage to calculate partial sum
                (counter, state, threadLocalStorage) =>
                {
                    threadLocalStorage += counter;
                    Console.WriteLine($"Task {Task.CurrentId} has sum {threadLocalStorage}");
                    return threadLocalStorage;

                },
                partialSum =>
                {
                    Console.WriteLine($"Partial value of task {Task.CurrentId} is {partialSum}");
                    Interlocked.Add(ref sum, partialSum);
                });

            Console.WriteLine($"Sum of 1..100 = {sum}");
        }
    }
}
