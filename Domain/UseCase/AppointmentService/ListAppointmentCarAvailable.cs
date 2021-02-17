using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;

namespace Domain.UseCase.AppointmentService
{
    public class ListAppointmentCarAvailable
    {
        private IAppointmentRepository<Appointment> _repository;

        public ListAppointmentCarAvailable(IAppointmentRepository<Appointment> repository)
        {
            _repository = repository;
        }

        public async Task<List<Appointment>> Execute(DateTime initialDate, DateTime finalDate)
        {
            return await _repository.FindAvailableCar(initialDate, finalDate);

        }
    }
}