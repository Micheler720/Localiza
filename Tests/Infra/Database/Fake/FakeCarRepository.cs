using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;

namespace Infra.Database.Fake
{
    public class FakeCarRepository : FakeBaseRepository<Car>, ICarRepository<Car>
    {
        public async Task<Car> FindByBoard(string board)
        {
            await Task.Delay(2000);
            return this._data
                    .Where( car => car.Board == board ).FirstOrDefault();
        }

        public async Task<Car> FindById(int id)
        {
            await Task.Delay(2000);
            return this._data
                    .Where( car => car.Id == id ).FirstOrDefault();
        }

        public async Task<Car> FindByIsBoardNotId(Car car)
        {
            await Task.Delay(2000);
            return this._data
                    .Where( carRepository => carRepository.Board == car.Board 
                        && car.Id != carRepository.Id
                    ).FirstOrDefault();
        }
    }
}