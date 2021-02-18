using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using Domain.Shared.Exceptions;

namespace Domain.UseCase.BrandServices
{
    public class CarBrandSaveService
    {
         
        private IBaseRegisterRepository<CarBrand> _repository;
        
        public CarBrandSaveService(IBaseRegisterRepository<CarBrand> repository)
        {
            this._repository = repository;
        }

        public async Task Execute (CarBrand register )
        {
            var registerExist = await _repository.FindByNotNameExist<CarBrand>( 
                "car_brands", register.Id, register.Name);

            if(registerExist != null) throw new RegisterExistException("Registro já cadastrado, não é possivel realizar o cadastro.");

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