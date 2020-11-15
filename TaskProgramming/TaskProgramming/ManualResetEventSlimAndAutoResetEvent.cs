using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class ManualResetEventSlimAndAutoResetEvent
    {
        static void ManualResetEventSlimAndAutoResetEventStart()
        {
            var evt = new ManualResetEventSlim(false);
            //var evt = new AutoResetEvent(false); //false

            Task.Factory.StartNew(() => {

                Console.WriteLine("Boiling water");
                evt.Set(); // true
            });

            var makeTea = Task.Factory.StartNew(() => {
                Console.WriteLine("Waiting for water ...");
                evt.Wait();
                //evt.WaitOne();//false false
                Console.WriteLine("Here is your tea.");
                //var ok = evt.WaitOne(1000);//false false
                var ok = evt.Wait(1000);// WaitOne = wait a signal to turn green one false false
                if (ok)
                {
                    Console.WriteLine("Enjoy your tea.");
                }
                else
                {
                    Console.WriteLine("No tea for you");
                }
            });

            makeTea.Wait();
        }
    }
}
