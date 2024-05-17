using Microsoft.EntityFrameworkCore;
using RegistrationPortal.Infrastructure.DataContext;
using RegistrationPortal.Infrastructure.GenericRepository.IRepoBase;
using System.Linq.Expressions;

namespace RegistrationPortal.Infrastructure.GenericRepository.RepositoryBase
{
    public sealed class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ProgramAppDbContext _context;
        public RepositoryBase(ProgramAppDbContext context)
        {
            _context = context;
        }
        public async Task<T> FindByKeysAsync(params object[] keyValues)
        {
            return await _context.Set<T>().FindAsync(keyValues);
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return trackChanges ? _context.Set<T>().Where(expression) :
                _context.Set<T>().Where(expression).AsNoTracking();
        }
        public IQueryable<T> FindAll(bool trackChanges)
        {
            return trackChanges ? _context.Set<T>() :
                _context.Set<T>().AsNoTracking();
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public async Task CreateManyAsync(IEnumerable<T> entity)
        {
            await _context.Set<T>().AddRangeAsync(entity);
        }

        public Task UpdateManyAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            return Task.CompletedTask;
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
