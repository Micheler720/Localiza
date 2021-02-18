using Domain.Entities;
using Domain.Repositories;
using Domain.ViewModel.Cars;
using System;
using System.Collections.Generic;
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
            return await _repository.FindByCarAvailable();
        }
    }
}
