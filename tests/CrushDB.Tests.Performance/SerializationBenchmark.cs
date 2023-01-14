using BenchmarkDotNet.Attributes;

namespace CrushDB.Tests.Performance
{
    [RPlotExporter]
    [MemoryDiagnoser]
    public class TestBenchmark
    {
        private object _anonObject;
        private object _largePayload;

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

            _largePayload = new LargePayloadWithHDImage();
        }

        [Benchmark, BenchmarkCategory("AnonType")]
        public string NewtonsoftAnon()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(_anonObject);
        }

        [Benchmark, BenchmarkCategory("AnonType")]
        public string SystemAnon()
        {
            return System.Text.Json.JsonSerializer.Serialize(_anonObject);
        }

        [Benchmark, BenchmarkCategory("Large")]
        public string NewtonsoftLarge()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(_largePayload);
        }

        [Benchmark, BenchmarkCategory("Large")]
        public string SystemLarge()
        {
            return System.Text.Json.JsonSerializer.Serialize(_largePayload);
        }

        private class LargePayloadWithHDImage
        {
            public LargePayloadWithHDImage()
            {
                Foo = Random.Shared.Next();
                Bytes = CreateRandomBytes(1280 * 720 * 3);
                ObjectList = CreateListOfObject(100);
                AnotherList = CreateListOfObject(100);
            }

            public int Foo { get; set; }

            public byte[] Bytes { get; set; }

            public List<object> ObjectList { get; set; }

            public List<object> AnotherList { get; set; }
        }

        private static List<object> CreateListOfObject(int size)
        {
            var result = new List<object>(size);

            for (var i = 0; i < size; ++i)
            {
                result.Add(new
                {
                    Index = i,
                    Id = Guid.NewGuid(),
                    GlobalId = Guid.NewGuid(),
                    Timestamp = DateTimeOffset.UtcNow,
                    TimestampString = DateTimeOffset.UtcNow.ToString("o"),
                });
            }

            return result;
        }

        private static byte[] CreateRandomBytes(int size)
        {
            var bytes = new byte[size];

            Random.Shared.NextBytes(bytes);

            return bytes;
        }
    }
}