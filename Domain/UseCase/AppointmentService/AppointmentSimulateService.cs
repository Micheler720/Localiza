using Domain.Entities;
using Domain.Repositories;
using Domain.Shared.Exceptions;
using Domain.ViewModel.Appointments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCase.AppointmentService
{
    public class AppointmentSimulateService
    {
        private IAppointmentRepository<Appointment> _repository;
        private ICarRepository<Car> _repositoryCar;

        public AppointmentSimulateService(IAppointmentRepository<Appointment> repository, ICarRepository<Car> repositoryCar)
        {
            _repository = repository;
            _repositoryCar = repositoryCar;
        }

        public async Task<AppointmentCreateView> Execute(int idCar, DateTime initialDate, DateTime finalDate)
        {
            var car = await _repositoryCar.FindById(idCar);
            if (car.Board == null) throw new NotFoundRegisterException("Carro não encontrado, verifique informações.");

            var span = finalDate.Subtract(initialDate);
            var hours = span.TotalHours;

            return new AppointmentCreateView()
            {
                DateTimeExpectedCollected = initialDate,
                DateTimeExpectedDelivery = initialDate,
                HourLocation = (int)hours,
                Subtotal = hours * car.HourPrice,
                IdCar = car.Id,
                HourPrice = car.HourPrice,
                Images = car.Photos.Split(',')
            };


        }
    }
}
