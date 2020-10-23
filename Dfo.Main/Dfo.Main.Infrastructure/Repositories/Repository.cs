using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Dfo.Main.Domain.Interfaces.Repositories;
using Dfo.Main.Infrastructure.Contexts;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Dfo.Main.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly SqlContext _context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(SqlContext sqlContext)
        {
            _context = sqlContext;
            DbSet = _context.Set<TEntity>();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual async Task Add(TEntity obj)
        {
            await Task.Run(() => DbSet.Add(obj));
            await _context.SaveChangesAsync();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            var entity = await DbSet.FindAsync(id);

            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual async Task<TEntity> GetFirst()
        {
            return await DbSet.FirstOrDefaultAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity obj)
        {
            await Task.Run(() => _context.Update(obj));
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetByFilter(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
    }
}
