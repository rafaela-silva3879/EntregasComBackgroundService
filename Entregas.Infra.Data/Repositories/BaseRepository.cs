using Entregas.Domain.Interfaces.Common;
using Entregas.Domain.Interfaces.Repositories;
using Entregas.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entregas.Infra.Data.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class
    {
        private readonly DataContext _dataContext;

        protected BaseRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            _dataContext.Entry(entity).State = EntityState.Added;
            await _dataContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            if (entity is IExcluivel excluivel)
            {
                excluivel.Excluido = true;
                _dataContext.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                _dataContext.Set<TEntity>().Remove(entity); // exclusão física
            }

            await _dataContext.SaveChangesAsync();
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _dataContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dataContext.Set<TEntity>().FindAsync(id);
        }

        public virtual void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
