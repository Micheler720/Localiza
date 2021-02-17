using System;
using System.Threading.Tasks;
using Domain.UseCase.ModelServices;
using Domain.Entities;
using Infra.Database.Fake;
using NUnit.Framework;
using Domain.Shared.Exceptions;

namespace DomainsFake.UseCase.ModelService
{
    public class CarModelSaveTest
    {
        private CarModelSaveService _service;
        private FakeBaseRepository<CarModel> _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new  FakeBaseRepository<CarModel>();
            this._service = new CarModelSaveService(_repository);
        }

        [Test]

        public async Task SaveSucess()
        {
            var carModel = new CarModel()
            {
                Name= "test-save"
            };
            Exception exception = null;
            try
            {
                await _service.Execute(carModel);
            }catch(Exception e)
            {
                exception = e;
            }

            Assert.AreEqual(exception, null);

        }

        [Test]

        public async Task NotSaveNameExist()
        {
            var carModel = new CarModel()
            {
                Name= "test-save"
            };
            await _repository.Add(carModel);
            Exception exception = null;
            try
            {
                await _service.Execute(carModel);
            }catch(RegisterExistException e)
            {
                exception = e;
            }

            Assert.AreNotEqual(exception, null);
            
        }
        
    }
}