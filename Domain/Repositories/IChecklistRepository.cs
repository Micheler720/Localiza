using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IChecklistRepository<CheckList> : IBaseRepository<CheckList> where CheckList : class
    {
        Task<CheckList> FindById(int id);
    }
}