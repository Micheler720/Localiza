using System.Threading.Tasks;
using Domain.Repositories;
using Domain.UseCase.UserServices.Exceptions;
using Domain.Entities;
using Domain.Entities.Roles;

namespace Domain.UseCase.UserServices
{
    public class OperatorSaveService
    {
        private IOperatorRepository<Operator> _repository;
        
        public OperatorSaveService(IOperatorRepository<Operator> repository)
        {
            this._repository = repository;
        }
        public async Task Execute(Operator operato )
        {
            if(operato.Registration == null) throw new UserNotDefinid(
                "Matricula de operador não foi definido."
                );

            Operator operatorExist;
            
            operato.UserRole = UserRole.Operator;  
            operatorExist = await _repository.FindByOperatorRegisterNot(operato);
            if(operatorExist != null) throw new UniqUserRegisterCpf("Usuário já registrado.");
            if(operato.Id == 0)
            {
                await this._repository.Add(operato);
                return;
            }              
            await this._repository.Update(operato);
            
        }
       
    }  
}