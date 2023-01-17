/*
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

*/

using Cassandra;
using CrushDB.Entities;

using var cluster = Cluster.Builder()
                     .AddContactPoints("cassandra")
                     .Build();

using var session = await cluster.ConnectAsync();

session.UserDefinedTypes.Define(UdtMap.For<BytesEntity>());

await session.ExecuteAsync(new SimpleStatement("CREATE KEYSPACE IF NOT EXISTS crushdb WITH replication = { 'class': 'SimpleStrategy', 'replication_factor': '1' }"));
await session.ExecuteAsync(new SimpleStatement("USE examples"));
await session.ExecuteAsync(new SimpleStatement("CREATE TABLE IF NOT EXISTS table_bytes_entity(partition_key text, row_key text, b, PRIMARY KEY(id))"));
