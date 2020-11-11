using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace TaskProgramming
{
    class ConcurrentQueueProgramming
    {
        public  void Main()
        {
            var q = new ConcurrentQueue<int>();
            q.Enqueue(1);
            q.Enqueue(2);

            // 2 1<- front

            int result;
            if (q.TryDequeue(out result))
            {
                Console.WriteLine($"Removed element {result}");
            }

            if (q.TryPeek(out result))
            {
                Console.WriteLine($"Front element is {result}");
            }
        }
    }
}
