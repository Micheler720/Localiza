using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;

namespace Infra.Database.Fake
{
    public class FakeOperatorRepository : FakeBaseRepository<Operator>, IOperatorRepository<Operator>
    {
        public async Task<Operator> FindByOperatorRegisterNot(Operator user)
        {
            await Task.Delay(2000);
            return this._data
                        .Where(data => user.Id != data.Id && data.Registration == user.Registration)
                        .FirstOrDefault();
        }

        public async Task<Operator> FindById(int id)
        {
            await Task.Delay(2000);
            return this._data
                        .Where(data => data.Id == id )
                        .FirstOrDefault();
        }

        public async Task<Operator> FindByRegistrationAndPassword(string registration, string password)
        {
            await Task.Delay(2000);
            return this._data
                        .Where(data => registration == data.Registration && data.Password == password)
                        .FirstOrDefault();
        }

        public async Task<List<Operator>> FindByOperator()
        {
            await Task.Delay(2000);
            return this._data;
        }
    }
}