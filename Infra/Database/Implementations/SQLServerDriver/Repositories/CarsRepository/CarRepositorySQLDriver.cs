using Domain.Entities;
using Domain.Repositories;
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
            Car car = await FindFirst(queryString, parameters);
            if (car.Id == 0) car = null;
            return car;
        }

        public async Task<Car> FindByIsBoardNotId(Car car)
        {
            var queryString = $"SELECT * FROM cars where board = @board and id <> @id limit 1";
            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter("@board", car.Board));
            parameters.Add(new SqlParameter("@id", car.Id));
            Car verifyCar = await FindFirst(queryString, parameters);
            if (verifyCar.Id == 0) car = null;
            return verifyCar;
        }


    }
}
