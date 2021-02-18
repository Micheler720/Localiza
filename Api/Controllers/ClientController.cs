using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.UseCase.UserServices;
using Domain.UseCase.UserServices.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Domain.ViewModel.Users;
using Infra.Authentication;
using Domain.UseCase.Builder;
using Infra.Database.Implementations.SQLServerDriver.Repositories.User;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientRepository<Client> _context;
        private readonly ClientSaveService _userSave;
        private readonly ClientListService _userList;
        private readonly ClientDeleteService _userDelete;
        private readonly ClientLoginService _clientLogin;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
            _context =  new ClientRepositorySQLDriver();
            _userSave = new ClientSaveService(_context);
            _userList = new ClientListService(_context);
            _clientLogin = new ClientLoginService(_context);
            _userDelete = new ClientDeleteService(_context);
        }


        [HttpGet]
        [Route("/clients")]
        [Authorize(Roles = "Operator")]
        public async Task<List<ClientView>> Get ()
        {
            return await this._userList.Execute();
        }

        [HttpPost]
        [Route("/clients")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] ClientSaveView userBody)
        {
            try
            {
                var user = EntityBuilder.Call<Client>(userBody);
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
        [Route("/clients/{id}")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Update([FromBody] ClientSaveView userBody, int id)
        {
            try
            {
                var user = EntityBuilder.Call<Client>(userBody);
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
        [Route("/clients/{id}")]
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
        [Route("/clients/login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody]ClientLogin user)
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
