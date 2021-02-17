using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using Domain.ViewModel.Shared;

namespace Domain.UseCase.BrandServices
{
    public class CarBrandListService
    {
        private IBaseRepository<CarBrand> _repository;
        
        public CarBrandListService(IBaseRepository<CarBrand> repository)
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