using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using Domain.ViewModel.Shared;

namespace Domain.UseCase.CarServices
{
    public class CategoryListService
    {
        private IBaseRepository<CarCategory> _repository;
        
        public CategoryListService(IBaseRepository<CarCategory> repository)
        {
            this._repository = repository;
        }

        public async Task<List<RegisterView>> Execute ()
        {
            var categories = await _repository.GetAll();
            var registerView = categories.Select( category =>
             new RegisterView() 
             {
                Id = category.Id, 
                Name = category.Name
             }).ToList();
            return registerView;
        }
    }
}