using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Dfo.Main.Domain.Interfaces.Repositories;
using Dfo.Main.Infrastructure.Contexts;
using System.Collections.Generic;

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
            return await DbSet.FindAsync(id);
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
            await Task.Run(() => DbSet.Update(obj));
            await _context.SaveChangesAsync();
        }
    }
}
