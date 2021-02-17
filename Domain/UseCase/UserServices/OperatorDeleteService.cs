using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using Domain.UseCase.UserServices.Exceptions;

namespace Domain.UseCase.UserServices
{
    public class OperatorDeleteService
    {
        private IOperatorRepository<Operator> _repository;
        
        public OperatorDeleteService(IOperatorRepository<Operator> repository)
        {
            this._repository = repository;
        }

        public async Task Execute(int id)
        {
            var user = await this._repository.FindById(id);
            if (id == 0) throw new UserNotFound("Operador não cadastrado.");
            var userRepository =  await this._repository.FindById(id);
            if(userRepository == null ) throw new UserNotFound("Operador não cadastrado.");
            await this._repository.Delete(userRepository);
        }
    }
}