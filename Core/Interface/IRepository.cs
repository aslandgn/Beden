using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IRepository<T> where T: class, IEntity, new()
    {
        Task<T> AddAsync(T entity);
        T Update(T entity);
        void Delete(T entity);
        Task<List<T>> GetListWithQueryAsync(Expression<Func<T, bool>> expresssion = null);
        Task<T> GetFirstOrDefaultWithQueryAsync(Expression<Func<T, bool>> expresssion);

    }
}
