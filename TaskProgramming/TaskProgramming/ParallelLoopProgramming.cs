using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class ParallelLoopProgramming
    {
         void ParallelLoopProgrammingMain()
        {
            static IEnumerable<int> Range(int start, int end, int step)
            {
                for (int i = start; i < end; i += step)
                {
                    yield return i;
                }
            }

            var a = new Action(() => Console.WriteLine($"First {Task.CurrentId}"));
            var b = new Action(() => Console.WriteLine($"Second {Task.CurrentId}"));
            var c = new Action(() => Console.WriteLine($"Third {Task.CurrentId}"));

            Parallel.Invoke(a, b, c);// invoke parallel

            //  for parrallel
            var po = new ParallelOptions();

            Parallel.For(1, 11, po, i =>
            {
                //Console.WriteLine($"{i * i} \t");
            });

            //foreach parrallel
            //string[] words = { "oh", "what", "a", "night" };
            //Parallel.ForEach(words, word =>
            //{
            //    Console.WriteLine($"{word} has length {word.Length} (task {Task.CurrentId})");
            //});

            Parallel.ForEach(Range(1, 20, 3), Console.WriteLine);
        }
    }
}
