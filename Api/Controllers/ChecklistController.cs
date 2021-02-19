using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Infra.Database.Implementations.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Domain.Repositories;
using Domain.UseCase.AppointmentService;
using Domain.Shared.Exceptions;
using Domain.UseCase.AppointmentService.Exceptions;
using Domain.UseCase.Builder;
using Domain.ViewModel.Appointments;
using Infra.Database.Implementations.SQLServerDriver.Repositories.AppointmentyRepository;
using Api;
using Infra.Services.PDFServices;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChecklistController : ControllerBase
    {
        private readonly ILogger<ChecklistController> _logger;
        private readonly IChecklistRepository<CheckList> _context;
        private readonly IAppointmentRepository<Appointment> _contextAppointment;
        private readonly CheckListSaveService _save;
        private readonly ChekListListService _list;
        private readonly CheckListDeleteService _delete;

        public ChecklistController(ILogger<ChecklistController> logger, ContextEntity context)
        {
            _logger = logger;
            _context =  new CheckListRepositorySQLDriver();
            _contextAppointment = new AppointmentRepositorySQLDriver();
            _save = new CheckListSaveService(_contextAppointment, _context, new PDFWriter());
            _list = new ChekListListService(_context);
            _delete = new CheckListDeleteService(_context);
        }

        [HttpGet]
        [Route("/appointments/checklist")]
        [Authorize(Roles = "Operator")]
        public async Task<List<CheckList>> Get ()
        {
            return await this._list.Execute();
        }

        [HttpPost]
        [Route("/appointments/checklist/{idAppointment}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Create([FromBody] CheckListSave checklistBody, int idAppointment)
        {
            try
            {
                var path = Startup.ContentRoot;
                var cheklist = EntityBuilder.Call<CheckList>(checklistBody);
                var pdfUrl = await _save.Execute(cheklist, idAppointment, checklistBody.DateTimeDelivery, path);                
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

        [HttpPut]
        [Route("/appointments/checklist/{idAppointment}/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Update([FromBody] CheckListSave checklistBody, int idAppointment, int id)
        {
            try
            {
                var path = Startup.ContentRoot;
                var checkList = EntityBuilder.Call<CheckList>(checklistBody);
                checkList.Id = id;                
                await _save.Execute(checkList, idAppointment, checklistBody.DateTimeDelivery, path);
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
            }
        }
        [HttpDelete]
        [Route("/appointments/checklist/{id}")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Delete(int id)
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
