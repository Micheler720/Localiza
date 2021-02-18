using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using Domain.Entities.Interfaces;
using Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using Domain.UseCase.UserServices;
using Domain.ViewModel.Users;

namespace Infra.Database.Implementations.EntityFramework.Repositories.UsersRespository
{
    public class ClientRepositoryEntity :  BaseEntityRepository<Client>, IClientRepository<Client>
    {
        
        public ClientRepositoryEntity(ContextEntity context) : base(context)
        {

            this._context = context;

        }


        public async Task<Client> FindByPersonRegisterNot( Client user)
        {
             var query = from u in _context.Clients
                where u.Cpf ==  user.Cpf
                && user.Id != u.Id
                select u;
            return await query.FirstOrDefaultAsync<Client>() as Client;
        }

        public async Task<Client> FindById(int id)
        {
            var query = from u in _context.Clients
                where  id == u.Id
                select u;
            return await query.FirstOrDefaultAsync<Client>() as Client;
        }

        public async Task<Client> FindByCpfAndPassword(string cpf, string password)
        {
            var query = from u in _context.Clients
                where  cpf == u.Cpf
                && password == u.Password
                select u;
            return await query.FirstOrDefaultAsync<Client>() as Client;
        }

        public async Task<List<Client>> FindByClient()
        {
            var userRole = UserRole.Client;
            var query = from u in _context.Clients
                where  u.UserRole == userRole
                select u;
            return await query.ToListAsync<Client>();
        }

        public Task<List<ClientAppointmentView>> FindByAppointmentCpf(string cpf)
        {
            throw new System.NotImplementedException();
        }
    }
}