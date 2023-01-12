using CrushDB.Entities;

namespace CrushDB.Repositories
{
    public interface IRepository<T>
        where T : IEntity
    {
        public Task SetAsync(T item);

        public Task<T> GetAsync(string key);

        public Task<IEnumerable<T>> GetRange(string startKey, string rangeEnd);
    }
}