using System.Linq.Expressions;

namespace Contracts;
public interface IRepositoryBase<T>
{
    Task<IEnumerable<T>> FindAllAsync();
    Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
    Task<T> FindByIdAsync(string id);
    Task<T> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(string id);
    Task DeleteAsync(T entity);
    Task<bool> ExistsAsync(string id);
    Task<long> CountAsync();
    Task<long> CountAsync(Expression<Func<T, bool>> expression);
}
