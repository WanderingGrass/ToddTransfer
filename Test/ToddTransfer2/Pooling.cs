using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToddTransfer2
{
    [MemoryDiagnoser]
    [Config(typeof(DontForceGcCollectionsConfig))] // we don't want to interfere with GC, we want to include it's impact
    public class Pooling
    {
        [Params((int)1E+2, // 100 bytes
            (int)1E+3, // 1 000 bytes = 1 KB
            (int)1E+4, // 10 000 bytes = 10 KB
            (int)1E+5, // 100 000 bytes = 100 KB
            (int)1E+6, // 1 000 000 bytes = 1 MB
            (int)1E+7)] // 10 000 000 bytes = 10 MB
        public int SizeInBytes { get; set; }

        private ArrayPool<byte> sizeAwarePool;

        [GlobalSetup]
        public void GlobalSetup()
            => sizeAwarePool = ArrayPool<byte>.Create(SizeInBytes + 1, 10); // let's create the pool that knows the real max size

        [Benchmark]
        public void Allocate()
            => DeadCodeEliminationHelper.KeepAliveWithoutBoxing(new byte[SizeInBytes]);

        [Benchmark]
        public void RentAndReturn_Shared()
        {
            var pool = ArrayPool<byte>.Shared;
            byte[] array = pool.Rent(SizeInBytes);
            pool.Return(array);
        }

        [Benchmark]
        public void RentAndReturn_Aware()
        {
            var pool = sizeAwarePool;
            byte[] array = pool.Rent(SizeInBytes);
            pool.Return(array);
        }
    }

    public class DontForceGcCollectionsConfig : ManualConfig
    {
        public DontForceGcCollectionsConfig()
        {
            Add(Job.Default
                .With(new GcMode()
                {
                    Force = false // tell BenchmarkDotNet not to force GC collections after every iteration
                }));
        }
    }
}
