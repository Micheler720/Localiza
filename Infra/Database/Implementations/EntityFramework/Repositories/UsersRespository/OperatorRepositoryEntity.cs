using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using Domain.Entities.Interfaces;
using Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database.Implementations.EntityFramework.Repositories.UsersRespository
{
    public class OperatorRepositoryEntity :  BaseEntityRepository<Operator>, IOperatorRepository<Operator>
    {
        
        public OperatorRepositoryEntity(ContextEntity context) : base(context)
        {

            this._context = context;

        }
        

        public async Task<Operator> FindByOperatorRegisterNot( Operator user)
        {
             var query = from u in _context.Operators
                where u.Registration ==  user.Registration
                && user.Id != u.Id
                select u;
            return await query.FirstOrDefaultAsync<Operator>() as Operator;
        }


        public async Task<Operator> FindById(int id)
        {
            var query = from u in _context.Operators
                where  id == u.Id
                select u;
            return await query.FirstOrDefaultAsync<Operator>() as Operator;
        }


        public async Task<Operator> FindByRegistrationAndPassword(string registration, string password)
        {
            var query = from u in _context.Operators
                where  registration == u.Registration
                && password == u.Password
                select u;
            return await query.FirstOrDefaultAsync<Operator>() as Operator;
        }

        public async Task<List<Operator>> FindByOperator()
        {
            var userRole = UserRole.Operator;
            var query = from u in _context.Operators
                where  u.UserRole == userRole
                select u;
            return await query.ToListAsync<Operator>();
        }

    }
}