using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Interfaces;
using Domain.Entities;
using Domain.Shared.Exceptions;

namespace Domain.UseCase.ModelServices
{
    public class CarModelSaveService
    {
         
        private IBaseRegisterRepository<CarModel> _repository;
        
        public CarModelSaveService(IBaseRegisterRepository<CarModel> repository)
        {
            this._repository = repository;
        }

        public async Task Execute (CarModel register )
        {
            var registerExist = await _repository.FindByNotNameExist<CarModel>( 
                "car_models", register.Id, register.Name);

            if(registerExist != null ) throw new RegisterExistException("Registro já cadastrado, não é possivel realizar o cadastro.");

            if(register.Id == 0 )
            {
                await this._repository.Add(register);
            }else
            {
                await this._repository.Update(register);                
            }

        }
    }
}