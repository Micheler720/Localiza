using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Repositories;
using Domain.Entities;
using Domain.ViewModel.Shared;

namespace Domain.UseCase.ModelServices
{
    public class CarModelListService
    {
        private IBaseRepository<CarModel> _repository;
        
        public CarModelListService(IBaseRepository<CarModel> repository)
        {
            this._repository = repository;
        }

        public async Task<List<RegisterView>> Execute ()
        {
            var registers = await _repository.GetAll();
            var registerView = registers.Select( category =>
             new RegisterView() 
             {
                Id = category.Id, 
                Name = category.Name
             }).ToList();
            return registerView;
        }
    }
}