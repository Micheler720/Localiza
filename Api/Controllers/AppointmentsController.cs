﻿using System;
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
using Api;
using Infra.Services.PDFServices;
using Domain.ViewModel.Users;
using Domain.UseCase.UserServices;

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
        private readonly ListAppointmentClientsService _userListAppointment;
        private readonly AppointmentSaveService _save;
        private readonly AppointmentListService _list;
        private readonly ListAppointmentByPeriod _listCarAvailable;
        private readonly AppointmentDeleteService _delete;
        private readonly AppointmentSimulateService _simulate;

        public AppointmentsController(ILogger<AppointmentsController> logger, ContextEntity context)
        {
            _logger = logger;
            _context =  new AppointmentRepositorySQLDriver();
            _contextClient = new ClientRepositorySQLDriver();
            _contextOperator = new OperatorRepositorySQLDriver();
            _contextCar = new CarRepositorySQLDriver();
            _save = new AppointmentSaveService(_context, _contextCar, _contextClient, _contextOperator, new PDFWriter());
            _list = new AppointmentListService(_context);
            _delete = new AppointmentDeleteService(_context);
            _listCarAvailable = new ListAppointmentByPeriod(_context);
            _userListAppointment = new ListAppointmentClientsService(_context);
            _simulate = new AppointmentSimulateService(_context, _contextCar);
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
                var path = Startup.ContentRoot;
                var pdfUrl = await _save.Execute(EntityBuilder.Call<Appointment>(appointmentBody), path);                
                return StatusCode(201, pdfUrl);
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

        [HttpPost]
        [Route("/appointments/simulator/{idCar}")]
        [AllowAnonymous]
        public async Task<AppointmentCreateView> Simulator([Required] DateTime initialDate, [Required] DateTime finalDate, int idCar)
        {
          return await _simulate.Execute(idCar, initialDate, finalDate);
        }

        [HttpGet]
        [Route("/appointments/clients/{Cpf}")]
        [Authorize(Roles = "Operator, Client")]
        public async Task<List<ClientAppointmentView>> AppointmentsCPF([Required] string Cpf)
        {
            return await _userListAppointment.Execute(Cpf);
        }

        [HttpPut]
        [Route("/appointments/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Update([FromBody]AppointmentUpdateView appointmentBody, [Required] int id)
        {
            try
            {
                var path = Startup.ContentRoot;
                var appointment = EntityBuilder.Call<Appointment>(appointmentBody);
                appointment.Id = id;             
                await _save.Execute(appointment, path );
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
