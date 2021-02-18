using Domain.Entities;
using Domain.Repositories;
using Domain.ViewModel.Appointments;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Database.Implementations.SQLServerDriver.Repositories.AppointmentyRepository
{
    public class AppointmentRepositorySQLDriver : BaseSQLServerRepository<Appointment>, IAppointmentRepository<Appointment>
    {
        public async Task<bool> CheckAvailabilityCar(int idCar, DateTime dateTimeExpectedCollected)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter("@idCar", idCar));
            parameters.Add(new SqlParameter("@dateTimeExpectedCollected", dateTimeExpectedCollected));
            var queryString = $"SELECT * FROM appointments where idCar = @idCar and dateTimeExpectedDelivery <= @dateTimeExpectedCollected;";
            Appointment ap = await FindFirst<Appointment>(queryString, parameters);
            return ap == null;
        }

        public async Task<bool> CheckAvailabilityClient(int idClient, DateTime dateTimeExpectedCollected)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter("@idClient", idClient));
            parameters.Add(new SqlParameter("@dateTimeExpectedCollected", dateTimeExpectedCollected));
            var queryString = $"SELECT * FROM appointments where idClient = @idClient and dateTimeExpectedDelivery <= @dateTimeExpectedCollected;";
            Appointment ap = await FindFirst<Appointment>(queryString, parameters);
            if (ap.Client == null) ap = null;
            return ap == null;
        }

        public async Task<List<SchedulesDayAvailable>> FindAppointmentByPeriod(DateTime initialDate, DateTime finalDate)
        {
            List<DbParameter> parameters = new List<DbParameter>
            {
                new SqlParameter("@initialDate", initialDate),
                new SqlParameter("@finalDate", finalDate)
            };
            var queryString = "SELECT a.Id as IdAppointment, " +
                "a.schedule as Schedule, " +
                "b.name as NameClient, " +
                "c.board as Board," +
                "a.Amount as Amount," +
                "a.DateTimeCollected as AppointmentCollected," +
                "a.DateTimeDelivery as AppointmentDelivery" +
                "  FROM appointments a " +
                " inner join clients b on b.id = a.idclient " +
                " inner join cars c on c.Id  = a.idCar " +
                " where schedule between @initialDate and @finalDate;";
            var ap = await FindList<SchedulesDayAvailable>(queryString, parameters);
            return ap ;
        }

    }
}
