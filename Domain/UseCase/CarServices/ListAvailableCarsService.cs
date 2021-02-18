using Domain.Entities;
using Domain.Repositories;
using Domain.ViewModel.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.CarServices
{
    public class ListAvailableCarsService
    {
        private ICarRepository<Car> _repository;

        public ListAvailableCarsService(ICarRepository<Car> repository)
        {
            this._repository = repository;
        }

        public async Task<List<ListAvailableCar>> Execute()
        {
            var cars = await _repository.FindByCarAvailable();
            var carsEdit = cars.Select(car =>
            {
                car.Images = car.Photos != null ? car.Photos.Split(',') : null;
                return car;
            }).ToList();
            return carsEdit;
        }
    }
}
