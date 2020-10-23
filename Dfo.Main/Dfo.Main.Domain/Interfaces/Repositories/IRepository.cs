using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dfo.Main.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> GetFirst();
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task Update(TEntity obj);
        Task Add(TEntity obj);
        Task<int> SaveChanges();
    }
}