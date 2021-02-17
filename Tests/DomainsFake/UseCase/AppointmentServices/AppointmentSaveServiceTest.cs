using System;
using System.Threading.Tasks;
using Domain.UseCase.AppointmentService;
using Domain.UseCase.AppointmentService.Exceptions;
using Domain.Entities;
using Infra.Database.Fake;
using NUnit.Framework;
using Domain.Shared.Exceptions;

namespace DomainsFake.UseCase.AppointmentServices
{
    public class AppointmentSaveServiceTest
    {
        private AppointmentSaveService _service;
        private FakeAppointmentRepository _repository;
        private FakeCarRepository _repositoryCar;
        private FakeClientRepository _repositoryClient;
        private FakeOperatorRepository _repositoryOperator;
        private FakeBaseRepository<CarCategory> _repositoryCategory;

        [SetUp]
        public void Setup()
        {
            _repository = new FakeAppointmentRepository();
            _repositoryCar = new FakeCarRepository();
            _repositoryClient = new FakeClientRepository();
            _repositoryOperator = new FakeOperatorRepository();
            _repositoryCategory = new FakeBaseRepository<CarCategory>();
            _service = new AppointmentSaveService(_repository, _repositoryCar, _repositoryClient, _repositoryOperator);

        }

        [Test]
        public async Task SaveAppointmentSuccess()
        {
            var car = new Car()
            {
                Id = 12,                
            };
            var client = new Client()
            {
                Id = 12,                
            };
            var op = new Operator()
            {
                Id = 12,                
            };
            await _repositoryCar.Add(car);
            await _repositoryClient.Add(client);
            await _repositoryOperator.Add(op);
            var appointment = new Appointment()
            {
                Id= 15,
                DateTimeExpectedCollected = DateTime.Now,
                DateTimeExpectedDelivery = DateTime.Now.AddDays(10),
                HourPrice = 10,
                HourLocation = 10,
                Subtotal = 100,
                Amount = 100,
                IdCar = 12,
                IdClient = 12,
                IdOperator = 12
            };
            Exception exception = null;
            try
            {
                await _service.Execute(appointment);
            }catch(Exception ex)
            {
                exception = ex;
            }

            Assert.AreEqual(exception, null);
        }

        [Test]
        public async Task NotSaveAppointmentOperatorNotRegister()
        {
            var client = new Client()
            {
                Id = 12,                
            };
            var car = new Car()
            {
                Id = 12,                
            };
            await _repositoryClient.Add(client);
            await _repositoryCar.Add(car);
            var appointment = new Appointment()
            {
                Id= 15,
                DateTimeExpectedCollected = DateTime.Now,
                DateTimeExpectedDelivery = DateTime.Now.AddDays(10),
                HourPrice = 10,
                HourLocation = 10,
                Subtotal = 100,
                Amount = 100,
                IdCar = 12,
                IdClient = 12,
                IdOperator = 12
            };
            Exception exception = null;
            try
            {
                await _service.Execute(appointment);
            }catch(NotFoundRegisterException ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }

        [Test]
        public async Task NotSaveAppointmentCarNotRegister()
        {
            var client = new Client()
            {
                Id = 12,                
            };
            var op = new Operator()
            {
                Id = 12,                
            };
            await _repositoryClient.Add(client);
            await _repositoryOperator.Add(op);
            var appointment = new Appointment()
            {
                Id= 15,
                DateTimeExpectedCollected = DateTime.Now.AddDays(2),
                DateTimeExpectedDelivery = DateTime.Now.AddDays(10),
                HourPrice = 10,
                HourLocation = 10,
                Subtotal = 100,
                Amount = 100,
                IdCar = 12,
                IdClient = 12,
                IdOperator = 12
            };
            Exception exception = null;
            try
            {
                await _service.Execute(appointment);
            }catch(NotFoundRegisterException ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }

        [Test]
        public async Task NotSaveAppointmentClientNotRegister()
        {
            var car = new Car()
            {
                Id = 12,                
            };
            var op = new Operator()
            {
                Id = 12,                
            };
            await _repositoryCar.Add(car);
            await _repositoryOperator.Add(op);
            var appointment = new Appointment()
            {
                Id= 0,
                DateTimeExpectedCollected = DateTime.Now.AddDays(5),
                DateTimeExpectedDelivery = DateTime.Now.AddDays(10),
                HourPrice = 10,
                HourLocation = 10,
                Subtotal = 100,
                Amount = 100,
                IdCar = 12,
                IdClient = 12,
                IdOperator = 12
            };
            Exception exception = null;
            try
            {
                await _service.Execute(appointment);
            }catch(NotFoundRegisterException ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }   
        [Test]
        public async Task NotSaveAppointmentValuesZero()
        {
            var client = new Client()
            {
                Id = 12,
            };
            var car = new Car()
            {
                Id = 12,                
            };
            var op = new Operator()
            {
                Id = 12,                
            };
            await _repositoryCar.Add(car);
            await _repositoryClient.Add(client);
            await _repositoryOperator.Add(op);
            var appointment = new Appointment()
            {
                Id= 15,
                DateTimeExpectedCollected = DateTime.Now,
                DateTimeExpectedDelivery = DateTime.Now.AddDays(10),
                HourPrice = 0,
                HourLocation = 0,
                Subtotal = 0,
                Amount = 0,
                IdCar = 12,
                IdClient = 12,
                IdOperator = 12
            };
            Exception exception = null;
            try
            {
                await _service.Execute(appointment);
            }catch(ValuesInvalidException ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }
        [Test]
        public async Task DateTimeColectedInvalid()
        {
            var client = new Client()
            {
                Id = 12,
            };
            var car = new Car()
            {
                Id = 12,                
            };
            var op = new Operator()
            {
                Id = 12,                
            };
            await _repositoryCar.Add(car);
            await _repositoryClient.Add(client);
            await _repositoryOperator.Add(op);
            var appointment = new Appointment()
            {
                Id= 0,
                DateTimeExpectedCollected = DateTime.Now.AddDays(20),
                DateTimeExpectedDelivery = DateTime.Now.AddDays(10),
                HourPrice = 15,
                HourLocation = 15,
                Subtotal = 15,
                Amount = 15,
                IdCar = 12,
                IdClient = 12,
                IdOperator = 12
            };
            Exception exception = null;
            try
            {
                await _service.Execute(appointment);
            }catch(DateTimeColectedInvalidException ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }

        [Test]
        public async Task NotRegisterDateTimeColledtInvalid()
        {
            var client = new Client()
            {
                Id = 12,
            };
            var car = new Car()
            {
                Id = 12,                
            };
            var op = new Operator()
            {
                Id = 12,                
            };
            await _repositoryCar.Add(car);
            await _repositoryClient.Add(client);
            await _repositoryOperator.Add(op);
            var appointment = new Appointment()
            {
                Id= 0,
                DateTimeExpectedCollected = new DateTime(1990,1,1),
                DateTimeExpectedDelivery = DateTime.Now.AddDays(10),
                HourPrice = 15,
                HourLocation = 15,
                Subtotal = 15,
                Amount = 15,
                IdCar = 12,
                IdClient = 12,
                IdOperator = 12
            };
            Exception exception = null;
            try
            {
                await _service.Execute(appointment);
            }catch(DateTimeColectedInvalidException ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }
        
        [Test]
        public async Task NotRegisterAppointmentClientAlreadyAppointment()
        {
            var client = new Client()
            {
                Id = 12,
            };
            var car = new Car()
            {
                Id = 12,                
            };
            var op = new Operator()
            {
                Id = 12,                
            };
            await _repositoryCar.Add(car);
            await _repositoryClient.Add(client);
            await _repositoryOperator.Add(op);
            var appointment = new Appointment()
            {
                Id= 0,
                DateTimeExpectedCollected = DateTime.Now.AddDays(1),
                DateTimeExpectedDelivery = DateTime.Now.AddDays(10),
                DateTimeCollected = null,
                HourPrice = 15,
                HourLocation = 15,
                Subtotal = 15,
                Amount = 15,
                IdCar = 12,
                IdClient = 12,
                IdOperator = 12
            };
            Exception exception = null;
            try
            {
                await _service.Execute(appointment);
                await _service.Execute(appointment);
            }catch(ClientNotAvalabityException ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }

        [Test]
        public async Task NotRegisterAppointmentCarNotColleted ()
        {
            var client = new Client()
            {
                Id = 12,
            };
            var car = new Car()
            {
                Id = 12,                
            };
            var car2 = new Car()
            {
                Id = 15,                
            };
            var op = new Operator()
            {
                Id = 12,                
            };
            await _repositoryCar.Add(car);
            await _repositoryCar.Add(car2);
            await _repositoryClient.Add(client);
            await _repositoryOperator.Add(op);
            var appointment = new Appointment()
            {
                Id= 0,
                DateTimeExpectedCollected = DateTime.Now.AddDays(1),
                DateTimeExpectedDelivery = DateTime.Now.AddDays(10),
                DateTimeCollected = null,
                HourPrice = 15,
                HourLocation = 15,
                Subtotal = 15,
                Amount = 15,
                IdCar = 12,
                IdClient = 12,
                IdOperator = 12
            };
            Exception exception = null;
            await _repository.Add(appointment);
            appointment.IdCar = 15;
            try
            {
                await _service.Execute(new Appointment()
            {
                Id= 0,
                DateTimeExpectedCollected = DateTime.Now.AddDays(1),
                DateTimeExpectedDelivery = DateTime.Now.AddDays(10),
                DateTimeCollected = null,
                HourPrice = 15,
                HourLocation = 15,
                Subtotal = 15,
                Amount = 15,
                IdCar = 15,
                IdClient = 12,
                IdOperator = 12
            });
            }catch(CarNotAvalabityException ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }

    }
}