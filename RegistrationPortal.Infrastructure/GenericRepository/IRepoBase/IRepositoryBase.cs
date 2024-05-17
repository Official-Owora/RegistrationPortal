using System.Linq.Expressions;

namespace RegistrationPortal.Infrastructure.GenericRepository.IRepoBase
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> FindByKeysAsync(params object[] keyValues);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        IQueryable<T> FindAll(bool trackChanges);
        Task CreateAsync(T entity);
        Task CreateManyAsync(IEnumerable<T> entity);
        Task UpdateManyAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
