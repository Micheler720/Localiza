using Domain.Entities;
using Domain.Repositories;
using Domain.Shared.Exceptions;
using Domain.ViewModel.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.UserServices
{
    public class ListAppointmentClientsService
    {
        
        private IAppointmentRepository<Appointment> _repository;

        public ListAppointmentClientsService(IAppointmentRepository<Appointment> repository)
        {
            _repository = repository;
        }
        public async Task<List<ClientAppointmentView>> Execute(string cpf)
        {
            if (cpf == null) throw new ParameterNotFoundException("CPF não definido para busca.");
            var appointments = await _repository.FindByAppointmentCpf(cpf);
            return appointments.Select(appointment =>
            {
                appointment.Images =  appointment.Photos.Split(',');
                return appointment;
            }).ToList();            

        }
    }
}
