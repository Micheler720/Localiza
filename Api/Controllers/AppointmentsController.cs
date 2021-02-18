using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.UseCase.UserServices.Exceptions;
using Domain.Entities;
using Infra.Database.Implementations.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Domain.Repositories;
using Domain.UseCase.AppointmentService;
using Domain.ViewModel.Appointments;
using Domain.UseCase.Builder;
using Domain.Shared.Exceptions;
using Domain.UseCase.AppointmentService.Exceptions;
using Infra.Database.Implementations.SQLServerDriver.Repositories.AppointmentyRepository;
using Infra.Database.Implementations.SQLServerDriver.Repositories.User;
using Infra.Database.Implementations.SQLServerDriver.Repositories.CarsRepository;
using Api.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly AppointmentRepositorySQLDriver _context;
        private readonly ICarRepository<Car> _contextCar;
        private readonly IClientRepository<Client> _contextClient;
        private readonly IOperatorRepository<Operator> _contextOperator;
        private readonly AppointmentSaveService _save;
        private readonly AppointmentListService _list;
        private readonly ListAppointmentByPeriod _listCarAvailable;
        private readonly AppointmentDeleteService _delete;

        public AppointmentsController(ILogger<AppointmentsController> logger, ContextEntity context)
        {
            _logger = logger;
            _context =  new AppointmentRepositorySQLDriver();
            _contextClient = new ClientRepositorySQLDriver();
            _contextOperator = new OperatorRepositorySQLDriver();
            _contextCar = new CarRepositorySQLDriver();
            _save = new AppointmentSaveService(_context, _contextCar, _contextClient, _contextOperator);
            _list = new AppointmentListService(_context);
            _delete = new AppointmentDeleteService(_context);
            _listCarAvailable = new ListAppointmentByPeriod(_context);
        }

        [HttpGet]
        [Route("/appointments")]
        [Authorize(Roles = "Operator")]
        public async Task<List<Appointment>> Get ()
        {
            return await this._list.Execute();
        }

        [HttpGet]
        [Route("/appointments/findAppointmentByPeriod")]
        [AllowAnonymous]
        public async Task<List<SchedulesDayAvailable>> GetTimeCourse ([Required][FromQuery] DateTime initialDate, [Required][FromQuery] DateTime finalDate) 
        {
            return await _listCarAvailable.Execute(initialDate, finalDate);
        }

        [HttpPost]
        [Route("/appointments")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Create([FromBody] AppointmentCreateView appointmentBody)
        {
            try
            {
                await _save.Execute(EntityBuilder.Call<Appointment>(appointmentBody));                
                return StatusCode(201);
            }
            catch(NotFoundRegisterException err)
            {
                return StatusCode(404, new {
                    Message = err.Message
                });
            }catch(CarNotAvalabityException err)
            {
                 return StatusCode(401, new {
                    Message = err.Message
                });
            }catch(ClientNotAvalabityException err)
            {
                 return StatusCode(401, new {
                    Message = err.Message
            });
            }catch(DateTimeColectedInvalidException err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }catch(ValuesInvalidException err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }

        [HttpPut]
        [Route("/appointments/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Update([FromBody]AppointmentUpdateView appointmentBody, [Required] int id)
        {
            try
            {   var appointment = EntityBuilder.Call<Appointment>(appointmentBody);
                appointment.Id = id;             
                await _save.Execute(appointment);
                return StatusCode(204);
            }
            catch(NotFoundRegisterException err)
            {
                return StatusCode(404, new {
                    Message = err.Message
                });
            }catch(DateTimeColectedInvalidException err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }catch(ValuesInvalidException err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }
        [HttpDelete]
        [Route("/appointments/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                await _delete.Execute(id);
                return StatusCode(204);
            }
            catch(UserNotFound err)
            {
                return StatusCode(404, new {
                    Message = err.Message
                });
            }
        }

        

        
    }
}
