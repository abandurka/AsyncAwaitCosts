using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace AsyncAwaitCosts.ConsoleAppBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<AsyncAwaitCosts>();
        }
    }

    // [SimpleJob(RuntimeMoniker.NetCoreApp22)]
    // [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    // [SimpleJob(RuntimeMoniker.Net50)]
    // [SimpleJob(RuntimeMoniker.Net60)]
    // [SimpleJob(RuntimeMoniker.Net70)]
    // [SimpleJob(RuntimeMoniker.Net80)]
    [ShortRunJob(RuntimeMoniker.Net80)]
    [MarkdownExporter]
    [MemoryDiagnoser]
    public class AsyncAwaitCosts
    {
        [Params(20, 200)]
        public int N;
        
        private readonly double salt = new Random().NextDouble(); 
        private IImmutableList<int> _inputData;
        
        [GlobalSetup]
        public void Setup()
        {
            _inputData = Enumerable.Range(1, N).ToImmutableList();
        }
        
        [Benchmark]
        public async Task<double> ReturnAwaitedTask()
        {
            var summ = 0d;
            for (var index = 0; index < _inputData.Count; index++)
            {
                var i = _inputData[index];
                summ += await CalculateAwaitTaskAsync(i);
            }

            return summ;
        }

        [Benchmark]
        public async Task<double> ReturnDoubleAwaitedTask()
        {
            var summ = 0d;
            for (var index = 0; index < _inputData.Count; index++)
            {
                var i = _inputData[index];
                summ += await CalculateDoubleAwaitedTaskAsync(i);
            }

            return summ;
        }
                
        [Benchmark]
        public async Task<double> ReturnDoubleTaskWithoutAwait()
        {
            var summ = 0d;
            for (var index = 0; index < _inputData.Count; index++)
            {
                var i = _inputData[index];
                summ += await CalculateDoubleNotAwaitedTaskAsync(i);
            }

            return summ;
        }

        [Benchmark]
        public async Task<double> ReturnTaskWithoutAwait()
        {
            var summ = 0d;
            for (var index = 0; index < _inputData.Count; index++)
            {
                var i = _inputData[index];
                summ += await CalculateNotAwaitedTaskAsync(i);
            }

            return summ;
        }
        
        [Benchmark]
        public async Task<double> ReturnTaskFromResult()
        {
            var summ = 0d;
            for (var index = 0; index < _inputData.Count; index++)
            {
                var i = _inputData[index];
                summ += await CalculateTaskFromResultAsync(i);
            }

            return summ;
        }
        
        [Benchmark]
        public async Task<double> ReturnResultWithAsyncWord()
        {
            var summ = 0d;
            for (var index = 0; index < _inputData.Count; index++)
            {
                var i = _inputData[index];
                summ += await CalculateAsyncResultWithoutTaskAsync(i);
            }

            return summ;
        }
        
        [Benchmark(Baseline = true)]
        public double For()
        {
            var summ = 0d;
            for (var index = 0; index < _inputData.Count; index++)
            {
                var i = _inputData[index];
                summ += Salt(i);
            }

            return summ;
        }
        
        private async Task<double> CalculateDoubleAwaitedTaskAsync(int i)
        {
            return await CalculateAwaitTaskAsync(i);
        }
        
        private async Task<double> CalculateAwaitTaskAsync(int i)
        {
            return await Task.Run(() => Salt(i));
        }
        
        private Task<double> CalculateDoubleNotAwaitedTaskAsync(int i)
        {
            return CalculateNotAwaitedTaskAsync(i);
        }
        
        private Task<double> CalculateNotAwaitedTaskAsync(int i)
        {
            return Task.Run(() => Salt(i));
        }
        
        private Task<double> CalculateTaskFromResultAsync(int i)
        {
            return Task.FromResult(Salt(i));
        }
        
        private async Task<double> CalculateAsyncResultWithoutTaskAsync(int i)
        {
            return Salt(i);
        }

        double Salt(int i)
        {
            return i * salt;
        }
    }
}
