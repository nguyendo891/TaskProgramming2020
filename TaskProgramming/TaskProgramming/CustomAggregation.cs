using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskProgramming
{
    class CustomAggregation
    {
        static void CustomAggregationMain()
        {
            // var sum = Enumerable.Range(1, 1000).Sum();
            // var sum = Enumerable.Range(1, 1000).Aggregate(0, (i, accumulator) => i + accumulator);
            var sum = ParallelEnumerable.Range(1, 1000)
                 .Aggregate(
                     0,
                     (partialSum, i) => partialSum += i,
                     (total, subtotal) => total += subtotal,
                     i => i);

            Console.WriteLine($"sum = {sum}");
        }
    }
}
