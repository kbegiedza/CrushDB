using System.Diagnostics;
using System.Text.Json;
using CrushDB.Entities;
using dotnet_etcd;
using Etcdserverpb;
using Google.Protobuf;

Console.WriteLine("Hello, World!");

var sampleEntity = new BytesEntity($"{Guid.NewGuid()}+{Guid.NewGuid()}", $"{Guid.NewGuid()}", 1024);

var client = new EtcdClient("http://etcd:2379,http://etcd:2380");
var item = JsonSerializer.Serialize(sampleEntity);
var stopwatch = Stopwatch.StartNew();

await client.PutAsync(sampleEntity.PartitionKey, item);

Console.WriteLine($"Single put: {stopwatch.Elapsed}");

var rangeRequest = new RangeRequest
{
    Key = ByteString.CopyFromUtf8("\0"),
    RangeEnd = ByteString.CopyFromUtf8("\0")
};

stopwatch = Stopwatch.StartNew();
var all = await client.GetAsync(rangeRequest);
stopwatch.Stop();

Console.WriteLine($"Get all: {stopwatch.Elapsed}");