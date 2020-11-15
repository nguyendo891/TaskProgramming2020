using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class AsyncFactoryMethod
    {
        public class Foo
        {
            private Foo()
            {

            }

            private async Task<Foo> InitAsync()
            {
                await Task.Delay(1000);
                return this;
            }
            public static Task<Foo> CreateAsync()
            {
                var result = new Foo();
                return result.InitAsync();
            }
        }
        public class AsyncFactoryMethodProgram
        {
            static  async void AsyncFactoryMethodMain(string[] args)
            {
                //var foo = new Foo();
                //await foo.InitAsync();

                var x = await Foo.CreateAsync();

            }
        }
    }
}
