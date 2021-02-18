using Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IBaseRegisterRepository<T> : IBaseRepository<T> where T : class
    {
        Task<T> FindByNotNameExist<T>(string tableName, int id, string name);
    }
}
