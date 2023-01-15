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

        private string _value;

        public EtcdBenchmark()
        {
            _value = string.Empty;

            _client = new EtcdClient("http://etcd:2379,http://etcd:2380");
        }

        [Params(1024, 2048, 4096, 8192)]
        public int ValueSize { get; set; }

        [Params(1, 10, 100, 1000)]
        public int Insertions { get; set; }

        [GlobalSetup]
        public async Task SetupAsync()
        {
            var range = new DeleteRangeRequest
            {
                Key = ByteString.CopyFromUtf8("\0"),
                RangeEnd = ByteString.CopyFromUtf8("\0")
            };

            await _client.DeleteAsync(range);

            var bytes = CreateRandomBytes(ValueSize);

            _value = JsonSerializer.Serialize(bytes);
        }

        [Benchmark]
        public async Task SetAsync()
        {
            for (var i = 0; i < Insertions; ++i)
            {
                await _client.PutAsync(i.ToString(), _value);
            }
        }

        private static byte[] CreateRandomBytes(int size)
        {
            var bytes = new byte[size];

            Random.Shared.NextBytes(bytes);

            return bytes;
        }
    }
}