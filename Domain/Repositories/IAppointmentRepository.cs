using Domain.ViewModel.Appointments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IAppointmentRepository<Appointment> : IBaseRepository<Appointment> where Appointment : class
    {
        Task<Boolean> CheckAvailabilityCar(int idCar, DateTime dateTimeExpectedCollected );
        Task<Boolean> CheckAvailabilityClient(int idClient, DateTime dateTimeExpectedCollected );
        Task<List<SchedulesDayAvailable>> FindAppointmentByPeriod(DateTime initialDate, DateTime finalDate );
        
    }
}