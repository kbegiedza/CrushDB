using CrushDB.Entities;

Console.WriteLine("Hello, World!");

var sampleEntity = new BytesEntity($"{Guid.NewGuid()}+{Guid.NewGuid()}", $"{Guid.NewGuid()}", 1024);