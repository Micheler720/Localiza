using System;
using System.Threading.Tasks;
using Domain.UseCase.BrandServices;
using Domain.UseCase.ModelServices;
using Domain.Entities;
using Infra.Database.Fake;
using NUnit.Framework;
using Domain.Shared.Exceptions;

namespace DomainsFake.UseCase.BrandService
{
    public class BrandDeleteTest
    {
        private CarBrandDeleteService _service;
        private FakeBaseRepository<CarBrand> _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new  FakeBaseRepository<CarBrand>();
            this._service = new CarBrandDeleteService(_repository);
        }

        [Test]
        public async Task DeleteModelSucess ()
        {
            var carBrand = new CarBrand()
            {
                Id = 15,                
            };

            await _repository.Add(carBrand);
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