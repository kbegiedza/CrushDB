namespace CrushDB.Entities
{
    public class BytesEntity
    {
        public BytesEntity(string partitionKey, string rowKey, int size)
        {
            RowKey = rowKey;
            PartitionKey = partitionKey;
            Bytes = new byte[size * 4];
        }

        public byte[] Bytes { get; set; }

        public string RowKey { get; set; }

        public string PartitionKey { get; set; }
    }
}