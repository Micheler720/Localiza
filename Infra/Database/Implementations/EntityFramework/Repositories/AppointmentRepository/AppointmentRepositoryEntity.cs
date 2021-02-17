using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Infra.Database.Implementations.EntityFramework.Repositories.AppointmentRepository
{
    public class AppointmentRepositoryEntity  : BaseEntityRepository<Appointment>, IAppointmentRepository<Appointment>
    {
        public AppointmentRepositoryEntity(ContextEntity context) : base(context)
        {

            this._context = context;

        }

        public async Task<Appointment> FindById(int id)
        {
            var query = from a in _context.Appointments
                where  id == a.Id
                select a;
            return await query.FirstOrDefaultAsync<Appointment>() as Appointment;
        }

        public async Task<Boolean> CheckAvailabilityCar(int idCar, DateTime DateTimeExpectedCollected )
        {
            var query = from a in _context.Appointments
                where  idCar == a.IdCar && a.DateTimeCollected == null
                && a.DateTimeExpectedDelivery < DateTimeExpectedCollected
                select a;
            return await query.FirstOrDefaultAsync<Appointment>() == null;
        }

        public async Task<Boolean> CheckAvailabilityClient(int idClient, DateTime DateTimeExpectedCollected )
        {
            var query = from a in _context.Appointments
                where  idClient == a.IdClient && a.DateTimeCollected == null
                && a.DateTimeExpectedDelivery < DateTimeExpectedCollected
                select a;
            return await query.FirstOrDefaultAsync<Appointment>() == null;
        }

        public async Task<List<Appointment>> FindAvailableCar(DateTime initialDate, DateTime finalDate)
        {
            var query = from a in _context.Appointments
                where a.DateTimeExpectedCollected < initialDate
                && a.DateTimeExpectedDelivery < finalDate
                select a;
            return await query.ToListAsync<Appointment>();
        }
    }
}