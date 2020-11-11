using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class ConcurrentBagProgramming
    {
        public  void Main()
        {
            // stack LIFO
            // queue FIFO
            // no ordering

            var bag = new ConcurrentBag<int>();
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                var i1 = i;
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    bag.Add(i1);
                    Console.WriteLine($"{Task.CurrentId} has added {i1}");
                    int result;
                    if (bag.TryPeek(out result))
                    {
                        Console.WriteLine($"{Task.CurrentId} has peeked the value {result}");
                    }
                }));


            }

            Task.WaitAll(tasks.ToArray());

            int last;
            // NOTE: As long as we don't care the order , then concurrent bag is a good choice.
            if (bag.TryTake(out last))
            {
                Console.WriteLine($"I got {last}");
            }
        }
    }
}
