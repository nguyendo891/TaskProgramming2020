using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class AsynchrousInitializationParttern
    {
        public interface IAsyncInit
        {
            Task InitTask { get; }
        }

        public class MyClass : IAsyncInit
        {
            public MyClass()
            {
                InitTask = InitAsync();
            }

            public Task InitTask { get; }
            private async Task InitAsync()
            {
                await Task.Delay(1000);
            }
        }

        public class MyOtherClass : IAsyncInit
        {
            private readonly MyClass _myClass;
            public MyOtherClass(MyClass myClass)
            {
                _myClass = myClass;
                InitTask = InitAsync();
            }

            public Task InitTask { get; }
            private async Task InitAsync()
            {

                if (_myClass is IAsyncInit ai)
                {
                    await ai.InitTask;
                }

                await Task.Delay(1000);
            }
        }
        public class AsynchrousInitializationPartternProgram
        {
            static async void AsynchrousInitializationPartternMain()
            {
                var myClass = new MyClass();
                var oc = new MyOtherClass(myClass);
                await oc.InitTask;
            }
        }
    }
}
