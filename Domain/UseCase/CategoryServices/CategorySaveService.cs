using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using Domain.UseCase.CategoryServices.Exceptions;

namespace Domain.UseCase.CategoryServices
{
    public class CategorySaveService
    {
         
        private IBaseRegisterRepository<CarCategory> _repository;
        
        public CategorySaveService(IBaseRegisterRepository<CarCategory> repository)
        {
            this._repository = repository;
        }

        public async Task Execute (CarCategory category )
        {
            var categoryExist = await _repository.FindByNotNameExist<CarCategory>( 
                "car_categories", category.Id, category.Name);

            if(categoryExist != null ) throw new CategoryExistException("Carro já cadastrado, não é possivel realizar o cadastro.");

            if(category.Id == 0 )
            {
                await this._repository.Add(category);
            }else
            {
                await this._repository.Update(category);                
            }

        }
    }
}