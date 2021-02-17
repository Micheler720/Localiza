using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using Domain.ViewModel.Appointments;

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
            return await _repository.FindAppointmentByPeriod(initialDate, finalDate);

        }
    }
}