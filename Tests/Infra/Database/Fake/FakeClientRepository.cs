using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;

namespace Infra.Database.Fake
{
    public class FakeClientRepository : FakeBaseRepository<Client>, IClientRepository<Client>
    {
        public async Task<List<Client>> FindByClient()
        {
            await Task.Delay(2000);
            return this._data;
        }

        public async Task<Client> FindByPersonRegisterNot(Client user)
        {
            
            await Task.Delay(2000);
            return this._data
                        .Where(data => user.Id != data.Id && data.Cpf == user.Cpf)
                        .FirstOrDefault();
        }

        public async Task<Client> FindById(int id)
        {
            await Task.Delay(2000);
            return this._data
                        .Where(data => data.Id == id )
                        .FirstOrDefault();
        }

        public async Task<Client> FindByCpfAndPassword(string cpf, string password)
        {
            await Task.Delay(2000);
            return this._data
                        .Where(data => data.Cpf == cpf && data.Password == password )
                        .FirstOrDefault();
        }
    }
}