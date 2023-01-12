using BenchmarkDotNet.Attributes;

namespace CrushDB.Tests.Performance
{
    public class TestBenchmark
    {
        private object _anonObject;

        public TestBenchmark()
        {
            _anonObject = new
            {
                Bytes = CreateRandomBytes(1024),
                Point = new
                {
                    X = 100,
                    Y = 200
                },
            };

            byte[] CreateRandomBytes(int size)
            {
                var bytes = new byte[size];

                Random.Shared.NextBytes(bytes);

                return bytes;
            }
        }

        [Benchmark]
        public string SerializeAnonTypeNewtonsoft()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(_anonObject);
        }

        [Benchmark]
        public string SerializeAnonTypeSystem()
        {
            return System.Text.Json.JsonSerializer.Serialize(_anonObject);
        }
    }
}