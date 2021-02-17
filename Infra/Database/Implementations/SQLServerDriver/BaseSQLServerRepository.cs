using Domain.Repositories;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Database.Implementations.SQLServerDriver
{
    public class BaseSQLServerRepository<T> : IBaseRepository<T>
    {

        public BaseSQLServerRepository()
        {
            string cnn = Environment.GetEnvironmentVariable("CONNECTION_STRING", EnvironmentVariableTarget.Process);
            if (string.IsNullOrEmpty(cnn))
            {
                cnn = "Server=tcp:localizabackend.database.windows.net,1433;Initial Catalog=databaseLocaliza;Persist Security Info=False;User ID=Administrador;Password=Administr@dor;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            }

            this.connectionString = cnn;
        }

        protected readonly string connectionString;

        public async Task Add(T entity)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            var queryString = MapTable.BuilderInsert(entity);
            SqlCommand command = new SqlCommand(queryString, connection);
            var parameters = MapTable.BuilderParameters(entity);
            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            command.Connection.Open();

            MapTable.SetIdOfEntity(entity, await command.ExecuteScalarAsync());
            await connection.CloseAsync();
        }

        public async Task Delete(T entity)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            var queryString = MapTable.BuilderDelete(entity);
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }

        
        public Task<List<T>> Filter(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] expressions)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAll(params Expression<Func<T, object>>[] expressions)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAll()
        {
            var list = new List<T>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var queryString = MapTable.BuilderSelect<T>();
                SqlCommand command = new SqlCommand(queryString, connection)
                {
                    CommandType = System.Data.CommandType.Text
                };
                command.Connection.Open();

                using SqlDataReader dr = command.ExecuteReader();
                while (await dr.ReadAsync())
                {
                    var instance = Activator.CreateInstance(typeof(T));
                    this.fill(instance, dr);
                    list.Add((T)instance);
                }
                await dr.CloseAsync();
                await dr.DisposeAsync();
            }


            return list;
        }

        protected void fill(object modelo, SqlDataReader dr)
        {
            foreach (var p in modelo.GetType().GetProperties())
            {
                try
                {
                    if (dr[p.Name] == DBNull.Value) continue;
                    p.SetValue(modelo, dr[p.Name]);
                }
                catch { }
            }
        }

        public async Task Update(T entity)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            var queryString = MapTable.BuilderUpdate(entity);
            SqlCommand command = new SqlCommand(queryString, connection);
            var parameters = MapTable.BuilderParameters(entity, true);
            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            command.Connection.Open();
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }

        public async Task<T> FindById(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            var instance = MapTable.CreateInstanceAndSetId<T>(id);
            var queryString = MapTable.BuildFindById<T>(id);
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Connection.Open();
            using SqlDataReader dr = command.ExecuteReader();
            if (await dr.ReadAsync())
                this.fill(instance, dr);

            await dr.CloseAsync();
            await dr.DisposeAsync();
            return instance;
        }

        protected async Task<List<T>> FindList<T>(string queryString, List<DbParameter> parameters = null)
        {
            List<T> data = new List<T>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection)
                {
                    CommandType = System.Data.CommandType.Text
                };

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                command.Connection.Open();

                using SqlDataReader dr = command.ExecuteReader();
                while (await dr.ReadAsync())
                {
                    var instance = Activator.CreateInstance(typeof(T));
                    this.fill(instance, dr);
                    data.Add((T)instance);
                }
                await dr.CloseAsync();
                await dr.DisposeAsync();
            }

            return data;
        }

        protected async Task<T> FindFirst(string queryString, List<DbParameter> parameters = null)
        {
            T data = default(T);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection)
                {
                    CommandType = System.Data.CommandType.Text
                };
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                command.Connection.Open();

                using SqlDataReader dr = command.ExecuteReader();
                while (await dr.ReadAsync())
                {
                    var instance = Activator.CreateInstance(typeof(T));
                    this.fill(instance, dr);
                    data = (T)instance;
                }
                await dr.CloseAsync();
                await dr.DisposeAsync();
            }

            return data;
        }

    }
}
