using Domain.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using System.Threading.Tasks;
using Domain.UseCase.UserServices.Exceptions;
using Domain.ViewModel.Users;

namespace Domain.UseCase.UserServices
{
    public class OperatorLoginService
    {
        private IOperatorRepository<Operator> _repository;
        
        public OperatorLoginService(IOperatorRepository<Operator> repository)
        {
            this._repository = repository;
        }

        public async Task<OperatorJWT> Login(OperatorLogin user, IToken token)
        {
            var hashPassword = new HashPasswordService();
            var passwordEncrypted = hashPassword.EncryptPassword(user.Password);
           var loggedUser = await _repository.FindByRegistrationAndPassword(user.Resgistration, passwordEncrypted);
           if(loggedUser == null) throw new UserNotFound("Usuário ou senha inválidos.");
           return new OperatorJWT(){
             Id = loggedUser.Id,
             Name = loggedUser.Name,
             Registration = loggedUser.Registration,
             Role = loggedUser.UserRole.ToString(),
             Token = token.TokenGenerateOperator(loggedUser)
           };
        }
    }
}