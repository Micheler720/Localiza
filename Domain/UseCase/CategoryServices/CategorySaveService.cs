using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using Domain.UseCase.CategoryServices.Exceptions;

namespace Domain.UseCase.CategoryServices
{
    public class CategorySaveService
    {
         
        private IBaseRepository<CarCategory> _repository;
        
        public CategorySaveService(IBaseRepository<CarCategory> repository)
        {
            this._repository = repository;
        }

        public async Task Execute (CarCategory category )
        {
            var categoryExist = await _repository.Filter( 
                c => c.Name == category.Name && c.Id != category.Id);

            if(categoryExist.Count > 0) throw new CategoryExistException("Carro já cadastrado, não é possivel realizar o cadastro.");

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