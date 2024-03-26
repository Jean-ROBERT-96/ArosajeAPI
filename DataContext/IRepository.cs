using Entities.Filters;

namespace DataContext
{
    public interface IRepository<T>
    {
        Task<T?> Get(Int64 id);
        Task<List<T>> Get(IFilter filter);
        Task<List<T>> GetAll();
        Task<T?> Post(T entity);
        Task<T?> Put(T entity);
        Task<T?> Delete(Int64 id);
    }
}
