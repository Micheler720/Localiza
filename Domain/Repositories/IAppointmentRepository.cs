using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IAppointmentRepository<Appointment> : IBaseRepository<Appointment> where Appointment : class
    {
        Task<Appointment> FindById(int id);
        Task<Boolean> CheckAvailabilityCar(int idCar, DateTime dateTimeExpectedCollected );
        Task<Boolean> CheckAvailabilityClient(int idClient, DateTime dateTimeExpectedCollected );
        Task<List<Appointment>> FindAvailableCar(DateTime initialDate, DateTime finalDate );
        
    }
}