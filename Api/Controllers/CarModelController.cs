using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Infra.Database.Implementations.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Infra.Database.Implementations.EntityFramework.Repositories;
using Domain.ViewModel.Shared;
using Domain.UseCase.ModelServices;
using Domain.Shared.Exceptions;
using Domain.UseCase.Builder;
using Infra.Database.Implementations.SQLServerDriver;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarModelController : ControllerBase
    {
        private readonly ILogger<CarModelController> _logger;
        private readonly BaseRegisterSQLRepository<CarModel> _context;
        private readonly CarModelSaveService _save;
        private readonly CarModelDeleteService _delete;
        private readonly CarModelListService _list;

        public CarModelController(ILogger<CarModelController> logger, ContextEntity context)
        {
            _logger = logger;
            _context = new BaseRegisterSQLRepository<CarModel>();
            _save = new CarModelSaveService(_context);
            _delete = new CarModelDeleteService(_context);
            _list = new CarModelListService(_context);
        }

        [HttpGet]
        [Route("/cars/models")]
        [AllowAnonymous]
        public async Task<List<RegisterView>> Get ()
        {
            return await this._list.Execute();
        }

        [HttpPost]
        [Route("/cars/models")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Create([FromBody] RegisterView register)
        {
            try
            {
                var category = EntityBuilder.Call<CarModel>(register);
                await _save.Execute(category);
                return StatusCode(201);
            }
            catch(RegisterExistException err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }

        [HttpPut]
        [Route("/cars/models/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Update([FromBody] RegisterView register, int id)
        {
            try
            {
                var model = EntityBuilder.Call<CarModel>(register);
                model.Id = id;
                await _save.Execute(model);
                return StatusCode(204);
            }
            catch(RegisterExistException err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }

        [HttpDelete]
        [Route("/cars/models/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Delete( int id)
        {
            try
            {
                await _delete.Execute(id);
                return StatusCode(204);
            }
            catch(NotFoundRegisterException err)
            {
                return StatusCode(404, new {
                    Message = err.Message
                });
            }
        }

        

        
    }
}
