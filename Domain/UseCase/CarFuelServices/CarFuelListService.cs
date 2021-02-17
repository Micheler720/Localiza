using Domain.Entities;
using Domain.Repositories;
using Domain.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.CarFuelServices
{
    public class CarFuelListService
    {
        private IBaseRepository<CarFuel> _repository;

        public CarFuelListService(IBaseRepository<CarFuel> repository)
        {
            this._repository = repository;
        }
        
        public async Task<List<RegisterView>> Execute()
        {
            var fuels = await _repository.GetAll();
            var registerView = fuels.Select(fuel =>
            new RegisterView()
            {
                Id = fuel.Id,
                Name = fuel.Name
            }).ToList();
            return registerView;
        }
    }
}
