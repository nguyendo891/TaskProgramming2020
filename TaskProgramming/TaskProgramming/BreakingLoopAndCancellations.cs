using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class BreakingLoopAndCancellations
    {
        private static ParallelLoopResult result;
        public static void BreakingLoopAndCancellationsDemo()
        {
            var cts = new CancellationTokenSource();
            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = cts.Token;
            result = Parallel.For(0, 20, po, (int x, ParallelLoopState state) =>
            {
                Console.WriteLine($"{x} [{Task.CurrentId}] \t");
                if (x == 10)
                {
                    //throw new Exception();
                    //state.Stop(); // it will not stop immediately , it has delay.
                    //state.Break();
                    cts.Cancel();
                }
            });

            Console.WriteLine();
            Console.WriteLine($"Was the loop completed ? {result.IsCompleted}");
            if (result.LowestBreakIteration.HasValue)
            {
                Console.WriteLine($"Lowest break iteration is {result.LowestBreakIteration}");
            }
        }

        static void BreakingLoopAndCancellationsMain()
        {
            try
            {
                BreakingLoopAndCancellationsDemo();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e.Message);
                    return true;
                });

            }
            catch (OperationCanceledException)
            {

            }
        }
    }
}
