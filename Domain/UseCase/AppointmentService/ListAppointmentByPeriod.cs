using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using Domain.ViewModel.Appointments;
using System.Linq;

namespace Domain.UseCase.AppointmentService
{
    public class ListAppointmentByPeriod
    {
        private IAppointmentRepository<Appointment> _repository;

        public ListAppointmentByPeriod(IAppointmentRepository<Appointment> repository)
        {
            _repository = repository;
        }

        public async Task<List<SchedulesDayAvailable>> Execute(DateTime initialDate, DateTime finalDate)
        {
            var appointments = await _repository.FindAppointmentByPeriod(initialDate, finalDate);
            return appointments.Select(appointment =>
            {
                appointment.Images = appointment.Photos.Split(',');
                return appointment;
            }).ToList();

        }
    }
}