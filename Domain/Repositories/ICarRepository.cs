using Domain.ViewModel.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICarRepository<Car> : IBaseRepository<Car> where Car : class
    {
        
        Task<Car> FindByBoard(string board);
        Task<Car> FindByIsBoardNotId(Car car);
        Task<List<ListAvailableCar>> FindByCarAvailable();
    }
}