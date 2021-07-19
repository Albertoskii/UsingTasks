using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace UsingTasks
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await SecuencialTasks(stopwatch);

            stopwatch.Restart();

            await ParallelTasks(stopwatch);

            Console.Read();
        }

        private static async Task ParallelTasks(Stopwatch stopwatch)
        {

            // this task will take about 2.5s to complete
            var sumTask = SlowAndComplexSumAsync();
     
            // this task will take about 4s to complete
            var wordTask = SlowAndComplexWordAsync();

            // running them in parallel should take about 4s to complete
            await Task.WhenAll(sumTask, wordTask);

            // The elapsed time at this point will be about 6.5s
            Console.WriteLine("Time elapsed when both complete..." + stopwatch.Elapsed);

            // These lines are to prove the outputs are as expected,
            // i.e. 300 for the complex sum and "ABC...XYZ" for the complex word
            Console.WriteLine("Result of complex sum = " + sumTask.Result);
            Console.WriteLine("Result of complex letter processing " + wordTask.Result);
        }

        private static async Task SecuencialTasks(Stopwatch stopwatch)
        {
            // This method takes about 2.5s to run
            var complexSum = await SlowAndComplexSumAsync(); //metodo externo: API, bd, computación whatever

            // The elapsed time will be approximately 2.5s so far
            Console.WriteLine("Time elapsed when sum completes..." + stopwatch.Elapsed);

            // This method takes about 4s to run
            var complexWord = await SlowAndComplexWordAsync();

            // The elapsed time at this point will be about 6.5s
            Console.WriteLine("Time elapsed when both complete..." + stopwatch.Elapsed);

            // These lines are to prove the outputs are as expected,
            // i.e. 300 for the complex sum and "ABC...XYZ" for the complex word
            Console.WriteLine("Result of complex sum = " + complexSum);
            Console.WriteLine("Result of complex letter processing " + complexWord);
        }

        private static async Task<string> SlowAndComplexWordAsync()
        {
            var word = string.Empty;
            foreach (var counter in Enumerable.Range(65, 26))
            {
                word = string.Concat(word, (char)counter);
                await Task.Delay(150);
            }

            return word;
        }

        private static async Task<int> SlowAndComplexSumAsync()
        {
            int sum = 0;
            foreach (var counter in Enumerable.Range(0, 25))
            {
                sum += counter;
                await Task.Delay(100);
            }

            return sum;
        }
    }
}
