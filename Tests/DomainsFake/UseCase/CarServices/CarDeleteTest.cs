using System;
using System.Threading.Tasks;
using Domain.UseCase.CarServices;
using Domain.UseCase.ModelServices;
using Domain.Entities;
using Infra.Database.Fake;
using NUnit.Framework;
using Domain.Shared.Exceptions;

namespace DomainsFake.UseCase.CarServices
{
    public class CarDeleteTest
    {
        private CarDeleteService _service;
        private FakeCarRepository _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new  FakeCarRepository();
            this._service = new CarDeleteService(_repository);
        }

        [Test]
        public async Task DeleteModelSucess ()
        {
            var model = new Car()
            {
                Id = 15,                
            };

            await _repository.Add(model);
            Exception exception = null;
            try
            {
                await _service.Execute(15);
            }catch (Exception ex)
            {
                exception = ex;
            }
            Assert.AreEqual(exception, null);
        }
        public async Task NotDeleteIdZero ()
        {
            Exception exception = null;
            try
            {
                await _service.Execute(0);
            }catch (NotFoundRegisterException ex)
            {
                exception = ex;
            }
            Assert.AreEqual(exception, null);
        }

        public async Task NotDeleteNotRegister ()
        {
            Exception exception = null;
            try
            {
                await _service.Execute(15);
            }catch (Exception ex)
            {
                exception = ex;
            }
            Assert.AreEqual(exception, null);
        }
    }
}