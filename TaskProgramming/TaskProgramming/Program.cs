using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Nito.AsyncEx;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace TaskProgramming
{
    public class Stuff
    {
        private static int value;

        private readonly Lazy<Task<int>> AutoIncValue =
            new Lazy<Task<int>>(async () => {
                await Task.Delay(1000).ConfigureAwait(false);
                return value++;

            });


        private readonly Lazy<Task<int>> AutoIncValue2 =
            new Lazy<Task<int>>( () => Task.Run(async() => {
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
    public class Program 
    {
        static  async void Main(string[] args)
        {
           
        }
    }
}
