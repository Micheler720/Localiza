using Domain.Entities;
using Domain.Repositories;
using Domain.ViewModel.Users;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Database.Implementations.SQLServerDriver.Repositories.User
{
    public class ClientRepositorySQLDriver : BaseSQLServerRepository<Client>, IClientRepository<Client>
    {
        public async Task<List<Client>> FindByClient()
        {
            var queryString = $"SELECT * FROM clients;";
            List<Client> clients = await FindList<Client>(queryString);
            return clients;
        }

        public async Task<Client> FindByCpfAndPassword(string cpf, string password)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter("@cpf", cpf));
            parameters.Add(new SqlParameter("@password", password));
            var queryString = $"SELECT * FROM clients where cpf = @cpf and password = @password;";
            Client client = await FindFirst<Client>(queryString, parameters);
            return client;
        }

        public async Task<Client> FindByPersonRegisterNot(Client user)
        {
            
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter("@cpf", user.Cpf));
            parameters.Add(new SqlParameter("@id", user.Id));
            var queryString = $"SELECT * FROM clients where id <> @id and cpf = @cpf ;";
            Client client =await FindFirst<Client>(queryString, parameters);
            return client;
        }

        public async Task<List<ClientAppointmentView>> FindByAppointmentCpf(string cpf)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter("@cpf", cpf));
            var queryString = $"SELECT a.Id as Id, " +
                $" a.Schedule as Schedule, " +
                $" b.Name as NameClient, " +
                $" c.Board as Board, " +
                $" d.Name as Category, " +
                $" a.Amount as Subtotal, " +
                $" a.HourPrice, " +
                $" a.HourLocation, " +
                $" a.AdditionalCosts," +
                $" a.DateTimeCollected as AppointmentCollected, " +
                $" a.DateTimeDelivery as AppointmentDelivery " +
                $" FROM appointments a " +
                $" inner join clients b on a.IdClient = b.Id " +
                $" inner join cars c on c.Id = a.IdCar " +
                $" inner join car_categories d on d.Id = c.IdCategory" +
                $" where b.Cpf = @cpf;";
            List<ClientAppointmentView> appointments = await FindList<ClientAppointmentView>(queryString, parameters);
            return appointments;
        }
    }
}
