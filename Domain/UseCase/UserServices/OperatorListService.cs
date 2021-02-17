using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using Domain.Entities.Interfaces;
using Domain.Entities.Roles;
using Domain.ViewModel.Users;

namespace Domain.UseCase.UserServices
{
    public class OperatorListService
    {
        private IOperatorRepository<Operator> _repository;
        
        public OperatorListService(IOperatorRepository<Operator> repository)
        {
            this._repository = repository;
        }
        public async Task<List<OperatorView>> Execute()
        {
            var users =  await _repository.FindByOperator();
            List<OperatorView> userView = users.Select( user =>                 
                new OperatorView()
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Registration = user.Registration,
                        } ).ToList();

            return userView;
            
        }
        
    }
}