using System.Text.Json;
using BenchmarkDotNet.Attributes;
using dotnet_etcd;
using Etcdserverpb;
using Google.Protobuf;

namespace CrushDB.Tests.Performance.Benchmarks
{
    public class EtcdBenchmark
    {
        private readonly EtcdClient _client;

        public EtcdBenchmark()
        {
            _client = new EtcdClient("http://etcd:2379,http://etcd:2380");
        }

        [Params()]
        public int ValueSize { get; set; }

        [GlobalSetup]
        public async Task SetupAsync()
        {
            var range = new DeleteRangeRequest
            {
                Key = ByteString.CopyFromUtf8("\0"),
                RangeEnd = ByteString.CopyFromUtf8("\0")
            };

            await _client.DeleteAsync(range);
        }

        [Benchmark]
        public async Task SetAsync()
        {
            /* await _client.PutAsync(, JsonSerializer.Serialize) */
        }
    }
}