using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.UseCase.UserServices.Exceptions;
using Domain.Entities;
using Infra.Database.Implementations.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Domain.UseCase.CarServices;
using Domain.ViewModel.Cars;
using Domain.UseCase.CarServices.Exceptions;
using Domain.UseCase.Builder;
using Infra.Database.Implementations.SQLServerDriver.Repositories.CarsRepository;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly ICarRepository<Car> _context;
        private readonly CarSaveService _save;
        private readonly CarListService _list;
        private readonly ListAvailableCarsService _listCarAvailable;
        private readonly CarDeleteService _delete;

        public CarController(ILogger<CarController> logger, ContextEntity context)
        {
            _logger = logger;
            _context =  new CarRepositorySQLDriver();
            _save = new CarSaveService(_context);
            _list = new CarListService(_context);
            _delete = new CarDeleteService(_context);
            _listCarAvailable = new ListAvailableCarsService(_context);
        }

        [HttpGet]
        [Route("/cars")]
        [AllowAnonymous]
        public async Task<List<Car>> Get ()
        {
            return await _list.Execute();
        }

        

        [HttpPost]
        [Route("/cars")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Create([FromBody] CarSaveView carBody)
        {
            try
            {
                carBody.Photos = carBody.Images.Count > 1 ? string.Join(',', carBody.Images) : carBody.Images[0];
                carBody.Images = null;
                var car = EntityBuilder.Call<Car>(carBody);
                await _save.Execute(car);
                return StatusCode(201);
            }
            catch(CarExistException err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }

        [HttpPut]
        [Route("/cars/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Update([FromBody]CarSaveView carBody, int id)
        {
            try
            {
                carBody.Photos = carBody.Images.Count > 1 ? string.Join(',', carBody.Images) : carBody.Images[0];
                carBody.Images = null;
                var car = EntityBuilder.Call<Car>(carBody);
                car.Id = id;
                await _save.Execute(car);
                return StatusCode(204);
            }
            catch(CarExistException err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }

        [HttpDelete]
        [Route("/cars/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _delete.Execute(id);
                return StatusCode(204);
            }
            catch(CarNotFoundException err)
            {
                return StatusCode(404, new {
                    Message = err.Message
                });
            }
        }
        [HttpGet]
        [Route("/cars/availableCars")]
        [AllowAnonymous]
        public async Task<List<ListAvailableCar>> AvalabileCar()
        {
            return await _listCarAvailable.Execute();
        }

    }
}
