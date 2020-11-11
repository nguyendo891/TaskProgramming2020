using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class WaitingATaskWithCancelationToken
    {
        public void WaitingTaskWithCts()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t = new Task(() =>
            {
                Console.WriteLine("I take 5 seconds");
                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("I'm done.");
            }, token);
            t.Start();

            Task t2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);
            //Console.ReadKey();
            //cts.Cancel();

            //if task.wait all  with token will throw exception aggregation if the task t t2 are cancelled subsequently.
            Task.WaitAll(new[] { t, t2 }, 4000, token);
            //show the status of task is whether cancel or not
            Console.WriteLine($"Task t status is {t.Status}");
            Console.WriteLine($"Task t2 status is {t2.Status}");

            // Task.WaitAll(t, t2);
            //t.Wait(token);
            // or Task.WaitAll() but it will wait all the tasks instead of waiting for the task has token

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
