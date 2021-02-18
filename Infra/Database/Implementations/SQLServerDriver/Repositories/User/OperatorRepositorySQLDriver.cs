using Domain.Entities;
using Domain.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Database.Implementations.SQLServerDriver.Repositories.User
{
    public class OperatorRepositorySQLDriver : BaseSQLServerRepository<Operator>, IOperatorRepository<Operator>
    {
        public async Task<List<Operator>> FindByOperator()
        {
            var queryString = $"SELECT * FROM operators;";
            List<Operator> operators =await FindList<Operator>(queryString);
            return operators;
        }

        public async Task<Operator> FindByOperatorRegisterNot(Operator user)
        {
            var queryString = $"SELECT * FROM operators where id <> @id and registration = @registration ;";         
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter("@registration", user.Registration));
            parameters.Add(new SqlParameter("@id", user.Id));
            Operator op = await FindFirst(queryString, parameters);            
            return op;
        }

        public async Task<Operator> FindByRegistrationAndPassword(string registration, string password)
        {
            var queryString = $"SELECT * FROM operators where registration = @registration and password = @password;";
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter("@registration", registration));
            parameters.Add(new SqlParameter("@password", password));
            Operator op = await FindFirst(queryString, parameters);            
            return op;
        }
    }
}
