using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class ChildTask
    {
        static void ChildTaskProgram()
        {
            var parent = new Task(() =>
            {
                //detached
                var child = new Task(() =>
                {
                    Console.WriteLine("child task starting");
                    Thread.Sleep(3000);
                    Console.WriteLine("child task finishing.");
                    throw new Exception();
                }, TaskCreationOptions.AttachedToParent);

                var completionHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Hooray, task{t.Id} 's state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

                var failHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Oops, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

                child.Start();
            });

            parent.Start();

            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {

                ae.Handle(e => true);
            }
        }
    }
}
