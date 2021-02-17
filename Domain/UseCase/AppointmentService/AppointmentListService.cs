using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;

namespace Domain.UseCase.AppointmentService
{
    public class AppointmentListService
    {

        private IAppointmentRepository<Appointment> _repository;

        public AppointmentListService(IAppointmentRepository<Appointment> repository)
        {
            _repository = repository;
        }

        public async Task<List<Appointment>> Execute()
        {
            return await _repository.GetAll();
        }
    }
}