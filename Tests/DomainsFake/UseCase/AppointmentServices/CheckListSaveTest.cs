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
    public class CheckListSaveTest
    {
        
        private CheckListSaveService _service;
        private FakeAppointmentRepository _repository;
        private FakeCheckListRepository _repositoryCheckList;

        [SetUp]
        public void Setup()
        {
            _repository = new FakeAppointmentRepository();
            _repositoryCheckList = new FakeCheckListRepository();
            _service = new CheckListSaveService(_repository, _repositoryCheckList);
        }

        [Test]
        public async Task SaveCheckListSucess ()
        {
            var appointment = new Appointment()
            {
                Id = 10,
                DateTimeCollected = new DateTime(2020,2,15)
            };
            var checkList = new CheckList()
            {
                Id = 5
            };
            await _repository.Add(appointment);
            Exception exception = null;
            try
            {
                await _service.Execute(checkList, 10, new DateTime(2020,2,19));
            }catch(Exception ex)
            {
                exception = ex;
            }

            Assert.AreEqual(exception, null);
        }

        [Test]
        public async Task NotSaveCheckListIdAppointmentZero ()
        {
            var checkList = new CheckList()
            {
                Id = 5
            };
            Exception exception = null;
            try
            {
                await _service.Execute(checkList, 0, new DateTime(2020,2,19));
            }catch(NotFoundRegisterException ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }

        [Test]
        public async Task NotSaveCheckListIdAppointmentNotRegister ()
        {
            var checkList = new CheckList()
            {
                Id = 5
            };
            
            Exception exception = null;
            try
            {
                await _service.Execute(checkList, 10, new DateTime(2020,2,19));
            }catch(NotFoundRegisterException ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }

        [Test]
        public async Task NotSaveCheckListIdDateTimeCollectedInvalid ()
        {
            var appointment = new Appointment()
            {
                Id = 10,
                DateTimeCollected = null
            };
            var checkList = new CheckList()
            {
                Id = 5
            };
            await _repository.Add(appointment);
            Exception exception = null;
            try
            {
                await _service.Execute(checkList, 10, new DateTime(2020,2,19));
            }catch(DateTimeColectedInvalidException ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }
        [Test]
        public async Task NotSaveCheckListIdDateTimeDeliveryInvalid ()
        {
            var appointment = new Appointment()
            {
                Id = 10,
                DateTimeCollected = new DateTime(2020,2,18)
            };
            var checkList = new CheckList()
            {
                Id = 5
            };
            await _repository.Add(appointment);
            Exception exception = null;
            try
            {
                await _service.Execute(checkList, 10, new DateTime(2020,2,01));
            }catch(DateTimeColectedInvalidException ex)
            {   
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }
    }
}