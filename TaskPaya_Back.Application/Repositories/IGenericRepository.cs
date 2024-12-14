using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskPaya_Back.Domain.Common;


namespace TaskPaya_Back.Application.Repositories
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetEntitiesQuery();
        Task<TEntity> GetEntityById(long entityId);
        Task<TEntity> GetEntityByIdNoTracking(long entityId);
        Task AddEntity(TEntity entity);
        void UpdateEntity(TEntity entity);
        void RemoveEntity(TEntity entity);
        Task RemoveEntity(long entityId);
        Task RemoveEntityForce(long entityId);
        Task SaveChanges();
    }
}
