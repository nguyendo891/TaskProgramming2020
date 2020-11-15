using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class BarierProgramming
    {
        static Barrier barrier = new Barrier(2, b =>
        {
            Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished");
            //step 3,step 9

        });

        public static void Water()
        {
            Console.WriteLine("Putting the kettle on ( takes a bit longer)");
            Thread.Sleep(2000);
            barrier.SignalAndWait(); //(step 2) then counter increase to 2, reach 2, display barier message
            Console.WriteLine("Pouring water into cup.");//(step 4) reset counter 0
            barrier.SignalAndWait();// (step 5) 1
            Console.WriteLine("Putting the kettle away");//(step 8)
        }

        public static void Cup()
        {
            Console.WriteLine("Finding the nicest cup of tea (fast)"); //step 0
            barrier.SignalAndWait(); //(step 1)counter start here 1
            Console.WriteLine("Adding tea.");
            barrier.SignalAndWait(); //(step 6) 2
            Console.WriteLine("Adding sugar."); //(step 7) 
        }

        static void MainT()
        {
            var water = Task.Factory.StartNew(Water);
            var cup = Task.Factory.StartNew(Cup);

            var tea = Task.Factory.ContinueWhenAll(new[] { water, cup }, tasks =>
            {
                Console.WriteLine("Enjoy your cup of tea."); //step10
            });
        }
    }
}
