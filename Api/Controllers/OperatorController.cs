using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.UseCase.UserServices;
using Domain.UseCase.UserServices.Exceptions;
using Domain.Entities;
using Infra.Database.Implementations.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Domain.ViewModel.Users;
using Infra.Authentication;
using Domain.Repositories;
using Domain.UseCase.Builder;
using Infra.Database.Implementations.SQLServerDriver.Repositories.User;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperatorController : ControllerBase
    {
        private readonly ILogger<OperatorController> _logger;
        private readonly IOperatorRepository<Operator> _context;
        private readonly OperatorSaveService _userSave;
        private readonly OperatorListService _userList;
        private readonly OperatorDeleteService _userDelete;
        private readonly OperatorLoginService _clientLogin;

        public OperatorController(ILogger<OperatorController> logger, ContextEntity context)
        {
            _logger = logger;
            _context =  new OperatorRepositorySQLDriver();
            _userSave = new OperatorSaveService(_context);
            _userList = new OperatorListService(_context);
            _clientLogin = new OperatorLoginService(_context);
            _userDelete = new OperatorDeleteService(_context);
        }

        [HttpGet]
        [Route("/operators")]
        [Authorize(Roles = "Operator")]
        public async Task<List<OperatorView>> Get ()
        {
            return await this._userList.Execute();
        }

        [HttpPost]
        [Route("/operators")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Create([FromBody] OperatorSaveView userBody)
        {
            try
            {
                var user = new Operator() 
                {  
                    Registration = userBody.Registration, 
                    Name = userBody.Name, 
                    Password = userBody.Password                 
                };
                await _userSave.Execute(user);
                return StatusCode(201);
            }
            catch(UniqUserRegisterCpf err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }

        [HttpPut]
        [Route("/operators/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Update([FromBody]OperatorSaveView userBody, int id)
        {
            try
            {
                var user = EntityBuilder.Call<Operator>(userBody);
                user.Id = id;
                await _userSave.Execute(user);
                return StatusCode(204);
            }
            catch(UniqUserRegisterCpf err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }
        [HttpDelete]
        [Route("/operators/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userDelete.Execute(id);
                return StatusCode(204);
            }
            catch(UserNotFound err)
            {
                return StatusCode(404, new {
                    Message = err.Message
                });
            }
        }

        [HttpPost]
        [Route("/operators/login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody]OperatorLogin user)
        {  
            try
            {
                return StatusCode(200, await _clientLogin.Login(user, new Authentication()));
            }
            catch(UserNotFound err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }

        
    }
}
