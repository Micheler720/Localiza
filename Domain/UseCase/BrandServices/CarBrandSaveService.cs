using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using Domain.Shared.Exceptions;

namespace Domain.UseCase.BrandServices
{
    public class CarBrandSaveService
    {
         
        private IBaseRepository<CarBrand> _repository;
        
        public CarBrandSaveService(IBaseRepository<CarBrand> repository)
        {
            this._repository = repository;
        }

        public async Task Execute (CarBrand register )
        {
            var registerExist = await _repository.Filter( 
                c => c.Name == register.Name && c.Id != register.Id);

            if(registerExist.Count > 0) throw new RegisterExistException("Registro já cadastrado, não é possivel realizar o cadastro.");

            if(register.Id == 0 )
            {
                await _repository.Add(register);
            }else
            {
                await _repository.Update(register);                
            }

        }
    }
}