using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Infra.Database.Fake
{
    public class FakeBaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected List<T> _data;

        public FakeBaseRepository()
        {
            this._data = new List<T>();
        }
        public async Task Add(T entity)
        {
            this._data.Add(entity);
            await Task.Delay(2000);            
        }

        public async Task Delete(T entity)
        {
            this._data.Remove(entity);
            await Task.Delay(2000);
        }

        public  async  Task<List<T>> Filter(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] expressions)
        {
            await Task.Delay(2000);
            return this._data;
        }

        public Task<T> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAll(params Expression<Func<T, object>>[] expressions)
        {
            await Task.Delay(2000);
            return this._data;
        }

        public async Task<List<T>> GetAll()
        {
            await Task.Delay(2000);
            return this._data;
        }

        public async Task Update(T entity)
        {
            await Task.Delay(2000);
            this._data.Clear();
            this._data.Add(entity);
        }
        
    }
}