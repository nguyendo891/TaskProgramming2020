using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class AsnychronousLazyInitialization
    {
        public class AsnychronousLazyInitializationStuff
        {
            private static int value;

            private readonly Lazy<Task<int>> AutoIncValue =
                new Lazy<Task<int>>(async () => {
                    await Task.Delay(1000).ConfigureAwait(false);
                    return value++;

                });


            private readonly Lazy<Task<int>> AutoIncValue2 =
                new Lazy<Task<int>>(() => Task.Run(async () => {
                    await Task.Delay(1000);
                    return value++;

                }));

            //Nito.AsyncEx
            public AsyncLazy<int> AutoIncValue3 = new AsyncLazy<int>(async () =>
            {
                await Task.Delay(1000);
                return value++;
            });
            public async void UseValue()
            {
                int value = await AutoIncValue.Value;
            }
        }
    }
}
