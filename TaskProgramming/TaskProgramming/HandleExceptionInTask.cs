using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class HandleExceptionInTask
    {
        private void Test()
        {
            var t = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Can't do this") { Source = "t" };
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't access this !") { Source = "t2" };
            });

            try
            {
                Task.WaitAll(t, t2);
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    ae.Handle(e =>
                    {
                        if (e is InvalidOperationException)
                        {
                            Console.WriteLine("Invalid op!");
                            return true;
                        }
                        else return false;
                    });
                }
            }
        }

        public void HandleUpperLevel()
        {
            try
            {
                Test();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    Console.WriteLine($"Handled elsewhere:{e.GetType()}");
                }

            }
        }
    }
}
