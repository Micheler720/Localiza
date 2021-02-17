using Domain.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using System.Threading.Tasks;
using Domain.UseCase.UserServices.Exceptions;
using Domain.ViewModel.Users;

namespace Domain.UseCase.UserServices
{
    public class ClientLoginService
    {
        private IClientRepository<Client> _repository;
        
        public ClientLoginService(IClientRepository<Client> repository)
        {
            this._repository = repository;
        }

        public async Task<ClientJWT> Login(ClientLogin user, IToken token)
        {
           var loggedUser = await _repository.FindByCpfAndPassword(user.Cpf, user.Password);
           if(loggedUser == null) throw new UserNotFound("Usuário ou senha inválidos.");
           return new ClientJWT(){
             Id = loggedUser.Id,
             Name = loggedUser.Name,
             Cpf = loggedUser.Cpf,
             Role = loggedUser.UserRole.ToString(),
             Token = token.TokenGenerateClient(loggedUser)
           };
        }
    }
}