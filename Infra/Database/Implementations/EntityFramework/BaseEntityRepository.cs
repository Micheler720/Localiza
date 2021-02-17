using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Infra.Database.Implementations.EntityFramework.Repositories
{
    public class BaseEntityRepository<T> : IBaseRepository<T> where T : class
    {
        protected ContextEntity _context;
        public BaseEntityRepository(ContextEntity context)
        {
         this._context = context;   
        }
        public async Task Add(T entity)
        {
            
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }
        

        public async Task Delete(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> Filter(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] expressions)
        {
            var query =  _context.Set<T>().Where(where);
            var teste = where.Body.GetType();
            foreach (var expression in expressions)
            {
                query = query.Include(expression);
            }
            return await query.ToListAsync();
        }

        public Task<T> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAll(params Expression<Func<T, object>>[] expressions)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var expression in expressions)
            {
                query = query.Include(expression);
            }
            return await query.ToListAsync();
        }

         public async Task<List<T>> GetAll()
        {
            var query = _context.Set<T>().AsQueryable();
            return await query.ToListAsync();
        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}