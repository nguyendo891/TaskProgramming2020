using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    public class TaskProgramming
    {
        [Benchmark]
        public void TaskProgrammingSquareEachValue()
        {
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];
            Parallel.ForEach(values, x => { results[x] = (int)Math.Pow(x, 2); });
        }

        [Benchmark]
        public void TaskProgrammingSquareEachValueChunked()
        {
            const int count = 100000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];
            var part = Partitioner.Create(0, count, 10000);
            Parallel.ForEach(part, range =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    results[i] = (int)Math.Pow(i, 2);
                }
            });
        }
        static void TaskProgrammingMain()
        {
            var summary = BenchmarkRunner.Run<TaskProgramming>();
            Console.WriteLine(summary);
        }
    }
    
}
