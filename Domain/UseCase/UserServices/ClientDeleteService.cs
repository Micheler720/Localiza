using System.Threading.Tasks;
using Domain.Repositories;
using Domain.UseCase.UserServices.Exceptions;
using Domain.Entities;
using Domain.Entities.Interfaces;

namespace Domain.UseCase.UserServices
{
    public class ClientDeleteService
    {
        private IClientRepository<Client> _repository;
        
        public ClientDeleteService(IClientRepository<Client> repository)
        {
            this._repository = repository;
        }

        public async Task Execute(int id)
        {
            var user = await this._repository.FindById(id);
            if (id == 0) throw new UserNotFound("Usuário não cadastrado.");
            var userRepository =  await this._repository.FindById(id);
            if(userRepository == null ) throw new UserNotFound("Usuário não cadastrado.");
            await this._repository.Delete(userRepository);
        }
    }
}