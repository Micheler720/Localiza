using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using Domain.Entities.Interfaces;
using Domain.Entities.Roles;
using Domain.UseCase.UserServices.Exceptions;

namespace Domain.UseCase.UserServices
{
    public class ClientSaveService
    {
        private IClientRepository<Client> _repository;
        
        public ClientSaveService(IClientRepository<Client> repository)
        {
            this._repository = repository;
        }
        public async Task Execute(Client client = null)
        {
            if(client.Cpf == null ) throw new UserNotDefinid(
                "CPF de cliente não foi definido."
                );

                IUser userExist;

                client.UserRole = UserRole.Client;
                userExist = await _repository.FindByPersonRegisterNot(client);
                if(userExist != null) throw new UniqUserRegisterCpf("Usuário já registrado.");

                if(client.Id == 0)
                {
                    await this._repository.Add(client);
                    return;
                }              
                await this._repository.Update(client);
            
        }
       
    }  
}