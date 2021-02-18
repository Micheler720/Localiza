using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using Domain.UseCase.CarServices.Exceptions;

namespace Domain.UseCase.CarServices
{
    public class CarSaveService
    {
         
        private ICarRepository<Car> _repository;
        
        public CarSaveService(ICarRepository<Car> repository)
        {
            this._repository = repository;
        }

        public async Task Execute (Car car)
        {
            var carExist = await _repository.FindByIsBoardNotId(car);

            if(carExist != null) throw new CarExistException("Carro já cadastrado, não é possivel realizar o cadastro.");

            if(car.Id == 0 )
            {
                await _repository.Add(car);
            }else
            {
                await _repository.Update(car);                
            }

        }
    }
}