using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Infra.Database.Implementations.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Domain.UseCase.CarServices;
using Domain.UseCase.CategoryServices;
using Domain.ViewModel.Shared;
using Domain.UseCase.CategoryServices.Exceptions;
using Domain.UseCase.Builder;
using Infra.Database.Implementations.SQLServerDriver;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarCategoryController : ControllerBase
    {
        private readonly ILogger<CarCategoryController> _logger;
        private readonly BaseSQLServerRepository<CarCategory> _context;
        private readonly CategorySaveService _save;
        private readonly CategoryDeleteService _delete;
        private readonly CategoryListService _list;

        public CarCategoryController(ILogger<CarCategoryController> logger, ContextEntity context)
        {
            _logger = logger;
            _context = new BaseSQLServerRepository<CarCategory>();
            _save = new CategorySaveService(_context);
            _delete = new CategoryDeleteService(_context);
            _list = new CategoryListService(_context);
        }

        [HttpGet]
        [Route("/cars/categories")]
        [AllowAnonymous]
        public async Task<List<RegisterView>> Get ()
        {
            return await this._list.Execute();
        }

        [HttpPost]
        [Route("/cars/categories")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Create([FromBody] RegisterView register)
        {
            try
            {
                var category = new CarCategory()
                {
                    Name = register.Name
                };
                await _save.Execute(category);
                return StatusCode(201);
            }
            catch(CategoryExistException err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }

        [HttpPut]
        [Route("/cars/categories/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Update([FromBody] RegisterView register, int id)
        {
            try
            {
                var category = EntityBuilder.Call<CarCategory>(register);
                category.Id = id;
                await _save.Execute(category);
                return StatusCode(204);
            }
            catch(CategoryExistException err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }

        [HttpDelete]
        [Route("/cars/categories/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Delete( int id)
        {
            try
            {
                await _delete.Execute(id);
                return StatusCode(204);
            }
            catch(CategoryNotFoundException err)
            {
                return StatusCode(404, new {
                    Message = err.Message
                });
            }
        }

        

        
    }
}
