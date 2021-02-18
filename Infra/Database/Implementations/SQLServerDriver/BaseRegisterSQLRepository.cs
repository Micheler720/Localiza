using Domain.Entities;
using Domain.Entities.Interfaces;
using Domain.Interfaces;
using Domain.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Database.Implementations.SQLServerDriver
{
    public class BaseRegisterSQLRepository
    {
        public void FindByNotNameExist<T>(string tableName,int id, string name)
        {
            var query = $"SELECT * FROM {tableName} where id != @id and name = @name ;";
            var param = new List<DbParameter>();
            param.Add(new SqlParameter("@id", id));
            param.Add(new SqlParameter("@name", name));
            //return await FindFirst<T>(query, param);
        }

    }
}
