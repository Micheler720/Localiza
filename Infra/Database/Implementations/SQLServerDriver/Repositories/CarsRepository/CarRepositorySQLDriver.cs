using Domain.Entities;
using Domain.Repositories;
using Domain.ViewModel.Cars;
using Infra.Database.Implementations.EntityFramework.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Database.Implementations.SQLServerDriver.Repositories.CarsRepository
{
    public class CarRepositorySQLDriver : BaseSQLServerRepository<Car>, ICarRepository<Car>
    {

        public async Task<Car> FindByBoard(string board)
        {
            var queryString = $"SELECT * FROM cars where board = @board ;";
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter("@board", board));
            Car car = await FindFirst<Car>(queryString, parameters);
            if (car.Id == 0) car = null;
            return car;
        }

        public async Task<List<ListAvailableCar>> FindByCarAvailable()
        {
            var queryString = $"SELECT b.Id as Id, " +
                "b.Board as Board, " +
                "b.HourPrice as HourPrice, " +
                "b.LuggageCapacity as LuggageCapacity," +
                "b.TankCapacity as TankCapacity," +
                "c.Name as Brand," +
                "d.Name as Category," +
                "e.Id as Fuel," +
                "f.Name as Model," +
                "f.Name as Model, " +
                "b.Photos as Photos," +
                "b.Name as Name, " +
                "appointments.DateTimeExpectedCollected as DateTimeExpectedNextCollected" +
                " FROM cars b " +
                " left join appointments on b.id = appointments.idCar" +
                $" and appointments.DateTimeCollected = '{default(DateTime)}'" +
                " inner join car_brands c on c.id = b.idBrand " +
                " inner join car_categories d on d.id = b.idCategory " +
                " inner join car_fuels e on e.id = b.idFuel " +
                " inner join car_models f on f.id = b.idModel ; " ;
            List<ListAvailableCar> cars = await FindList<ListAvailableCar>(queryString);
            return cars;
        }

        public async Task<Car> FindByIsBoardNotId(Car car)
        {
            var queryString = "SELECT * FROM cars where board = @board and id <> @id ;";
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter("@board", car.Board));
            parameters.Add(new SqlParameter("@id", car.Id));
            Car verifyCar = await FindFirst<Car>(queryString, parameters);
            return verifyCar;
        }


    }
}
