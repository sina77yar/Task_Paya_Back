

using CleanArchitecture.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using TaskPaya_Back.Application.Repositories;
using TaskPaya_Back.Domain.Common;

namespace TaskPaya_Back.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private PayaTaskDataContext _dbContext;
        private DbSet<TEntity> _dbSet;
        public GenericRepository(PayaTaskDataContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }


        public async Task AddEntity(TEntity entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.LastUpdateDate = entity.CreateDate;
            await _dbSet.AddAsync(entity);
        }

        public IQueryable<TEntity> GetEntitiesQuery()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<TEntity> GetEntityById(long entityId)
        {
            var res = await _dbSet.SingleOrDefaultAsync(e => e.Id == entityId);
            return  res;
        }
        public async Task<TEntity> GetEntityByIdNoTracking(long entityId)
        {
            var res = await _dbSet.AsNoTracking().SingleOrDefaultAsync(e => e.Id == entityId);
            return res;
        }
        public void RemoveEntity(TEntity entity)
        {
            entity.IsDeleted = true;
            _dbSet.Update(entity);
        }

        public async Task RemoveEntity(long entityId)
        {
            var entity = await GetEntityById(entityId);
            RemoveEntity(entity);
        }
        public async Task RemoveEntityForce(long entityId)
        {
            var entity = await GetEntityById(entityId);
            
            _dbSet.Remove(entity);
        }
        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void UpdateEntity(TEntity entity)
        {
            _dbSet.Update(entity);
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}
