using Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Concrate
{
    public class EfRepository<T, TContext> : IRepository<T> where T : class, IEntity, new() where TContext : DbContext
    {
        private TContext _context;

        public EfRepository(TContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<T> GetFirstOrDefaultWithQueryAsync(Expression<Func<T, bool>> expresssion)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expresssion);
        }

        public async Task<List<T>> GetListWithQueryAsync(Expression<Func<T, bool>> expresssion = null)
        {
            return await _context.Set<T>().Where(expresssion).ToListAsync();
        }

        public T Update(T entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }
    }
}
