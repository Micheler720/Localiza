using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using System.Linq;

namespace Domain.UseCase.CarServices
{
    public class CarListService
    {
        private ICarRepository<Car> _repository;
        
        public CarListService(ICarRepository<Car> repository)
        {
            this._repository = repository;
        }

        public async Task<List<Car>> Execute ()
        {
            var cars = await _repository.GetAll();
            var carsEdit = cars.Select((car) =>
            {
                car.Images = car.Photos != null ? car.Photos.Split(','): null;
                return car;
            }).ToList();
            return (List<Car>)carsEdit;
        }
    }
}