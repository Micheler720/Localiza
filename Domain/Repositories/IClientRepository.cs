using Domain.ViewModel.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IClientRepository<Client> : IBaseRepository<Client> where Client : class
    {
        Task<Client> FindByPersonRegisterNot(Client user);
        Task<Client> FindByCpfAndPassword(string cpf, string password);
        Task<List<Client>> FindByClient();
    }
}