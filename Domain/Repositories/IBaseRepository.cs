using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IBaseRepository<T>
    {        
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<List<T>> GetAll(params Expression<Func<T, object>>[] expressions);
        Task<List<T>> GetAll();
        Task<T> FindById(int id);
        Task<List<T>> Filter(
            Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] expressions
        );
    }
}