using Domain.Entities;
using Domain.Repositories;
using Domain.UseCase.CarFuelServices;
using Domain.ViewModel.Shared;
using Infra.Database.Implementations.SQLServerDriver;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuelController : Controller
    {
        private readonly ILogger<FuelController> _logger;
        private readonly IBaseRepository<CarFuel> _context;
        private readonly CarFuelListService _listService;

        public FuelController(ILogger<FuelController> logger)
        {
            _logger = logger;
            _context = new BaseSQLServerRepository<CarFuel>();
            _listService = new CarFuelListService(_context);
        }

        [HttpGet]
        [Route("/fuels")]
        [AllowAnonymous]
        public async Task<List<RegisterView>> Get()
        {
            return await _listService.Execute();
        }
    }
}
