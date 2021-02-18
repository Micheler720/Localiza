using Domain.Entities;
using Domain.Repositories;
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
            this._repository = repository;
        }
        public async Task<List<ClientAppointmentView>> Execute(string cpf)
        {
            return await _repository.FindByAppointmentCpf(cpf);            

        }
    }
}
