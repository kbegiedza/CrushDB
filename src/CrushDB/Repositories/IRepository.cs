using CrushDB.Entities;

namespace CrushDB.Repositories
{
    public interface IRepository<T>
        where T : IEntity
    {
        public Task InsertAsync(T item);

        public Task InsertAsync(T item);
    }
}