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
        
        private IClientRepository<Client> _repository;

        public ListAppointmentClientsService(IClientRepository<Client> repository)
        {
            _repository = repository;
        }
        public async Task<List<ClientAppointmentView>> Execute(string cpf)
        {
            if (cpf == null) throw new ParameterNotFoundException("CPF não definido para busca.");
            return await _repository.FindByAppointmentCpf(cpf);            

        }
    }
}
