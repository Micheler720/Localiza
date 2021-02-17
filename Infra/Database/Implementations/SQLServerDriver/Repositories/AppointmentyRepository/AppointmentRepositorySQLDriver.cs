﻿using Domain.Entities;
using Domain.Repositories;
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
            Appointment ap = await FindFirst(queryString, parameters);
            if (ap.Client == null) ap = null;
            return ap == null;
        }

        public async Task<bool> CheckAvailabilityClient(int idClient, DateTime dateTimeExpectedCollected)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter("@idClient", idClient));
            parameters.Add(new SqlParameter("@dateTimeExpectedCollected", dateTimeExpectedCollected));
            var queryString = $"SELECT * FROM appointments where idClient = @idClient and dateTimeExpectedDelivery <= @dateTimeExpectedCollected;";
            Appointment ap = await FindFirst(queryString, parameters);
            if (ap.Client == null) ap = null;
            return ap == null;
        }

        public Task<List<Appointment>> FindAvailableCar(DateTime initialDate, DateTime finalDate)
        {
            throw new NotImplementedException();
        }

    }
}