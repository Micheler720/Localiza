using System;
using System.Threading.Tasks;
using Domain.UseCase.AppointmentService;
using Domain.Entities;
using Infra.Database.Fake;
using NUnit.Framework;

namespace DomainsFake.UseCase.AppointmentServices
{
    public class AppointmentDeleteTest
    {
        private AppointmentDeleteService _service;
        private FakeAppointmentRepository _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new FakeAppointmentRepository();
            this._service = new AppointmentDeleteService(_repository);

        }

        [Test]
        public async Task DeleteAppointmentSuccess()
        {
            var appointment = new Appointment()
            {
                Id= 15,
                DateTimeCollected = DateTime.Now,
                DateTimeDelivery = DateTime.Now
            };
            await this._repository.Add(appointment);

            Exception exception = null;
            try
            {
                await this._service.Execute(appointment.Id);
            }catch(Exception ex)
            {
                exception = ex;
            }

            
            Assert.AreEqual(exception, null);
        }

        [Test]
        public async Task NotDeleteAppointmentId0()
        {
            var appointment = new Appointment()
            {
                Id= 0,
                DateTimeCollected = DateTime.Now,
                DateTimeDelivery = DateTime.Now
            };
            Exception exception = null;
            try
            {
                await this._service.Execute(appointment.Id);
            }catch(Exception ex)
            {
                exception = ex;
            }

            
            Assert.AreNotEqual(exception, null);
        }

        
        [Test]
        public async Task NotDeleteAppointmentNotRegister()
        {
            var appointment = new Appointment()
            {
                Id= 12,
                DateTimeCollected = DateTime.Now,
                DateTimeDelivery = DateTime.Now
            };            
            Exception exception = null;
            try
            {
                await this._service.Execute(appointment.Id);
            }catch(Exception ex)
            {
                exception = ex;
            }

            
            Assert.AreNotEqual(exception, null);
        }

    }
}