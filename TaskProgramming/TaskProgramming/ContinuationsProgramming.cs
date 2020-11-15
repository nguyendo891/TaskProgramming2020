using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class ContinuationsProgramming
    {
        static void Continuations()
        {
            /*var task = Task.Factory.StartNew(() => {
                Console.WriteLine("Boiling Water");
            });
            
            var task2 = task.ContinueWith(t => {
                Console.WriteLine($"Completed task {t.Id}, pour water into cup.");
            });

            task2.Wait();*/
            var task = Task.Factory.StartNew(() => "Task 1");
            var task2 = Task.Factory.StartNew(() => "Task 2");

            var task3 = Task.Factory.ContinueWhenAny(new[] { task, task2 }, task => {
                Console.WriteLine("Tasks completed:");
                // foreach (var t in tasks) {
                Console.WriteLine(" - " + task.Result);
                Console.WriteLine("All tasks done");
                //}

            });

            task3.Wait();
        }
    }
}
