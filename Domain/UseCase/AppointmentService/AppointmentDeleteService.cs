using System.Threading.Tasks;
using Domain.Entities;
using Domain.Shared.Exceptions;
using Domain.Repositories;
using Domain.UseCase.AppointmentService.Exceptions;

namespace Domain.UseCase.AppointmentService
{
    public class AppointmentDeleteService
    {
        private IAppointmentRepository<Appointment> _repository;

        public AppointmentDeleteService(IAppointmentRepository<Appointment> repository)
        {
            _repository = repository;
        }

        public async Task Execute(int id)
        {
            
            if(id == 0) throw new NotFoundRegisterException("Agendamento não Encontrado.");
            var appointment = await _repository.FindById(id);
            if(appointment == null ) throw new NotFoundRegisterException("Agendamento não Encontrado.");
            if(appointment.DateTimeCollected != null && appointment.DateTimeDelivery == null ) throw new AppointmentNotExcludeException("Carro possui agendamento em aberto. Verifique para exclusão.");
            await _repository.Delete(appointment);
        }
    }
}