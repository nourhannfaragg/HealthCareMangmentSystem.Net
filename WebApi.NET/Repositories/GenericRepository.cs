using Microsoft.EntityFrameworkCore;
using WebApi.NET.Data;
using WebApi.NET.Models;

namespace WebApi.NET.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T:BaseEntity
    {
        private readonly ApplicationContext _dbContext;

        private GenericRepository(ApplicationContext context)=> _dbContext= context;

        public async Task<T> CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _dbContext.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id)
            => await _dbContext.Set<T>().FindAsync(id);

        public async Task<bool> UpdateAsync(T entity)
        {
            var model = await GetByIdAsync(entity.Id);

            if (model is null) return false;

            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity is null) return false;

            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        

    }
}
