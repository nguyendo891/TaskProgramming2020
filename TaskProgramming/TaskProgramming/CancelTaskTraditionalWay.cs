using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgramming
{
    public class CancelTaskTraditionalWay
    {
        public void CancelTask()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            //handle cancel action
            token.Register(() =>
            {
                Console.WriteLine("Cancellation has been requested.");

            });

            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    //if (token.IsCancellationRequested)
                    //{
                    //    throw new OperationCanceledException();
                    //}
                    //else
                    //{
                    //    Console.WriteLine($"{i++}\t");
                    //}
                    //OR
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                }
            }, token);
            t.Start();

            //wait for cancel
            Task.Factory.StartNew(() =>
            {
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait handle released, cancelation was requested");
            });

            Console.ReadKey();
            cts.Cancel();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
