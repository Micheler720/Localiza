using System;
using System.Threading.Tasks;
using Domain.UseCase.BrandServices;
using Domain.Entities;
using Infra.Database.Fake;
using NUnit.Framework;
using Domain.Shared.Exceptions;

namespace DomainsFake.UseCase.ModelService.BrandService
{
    public class BrandSaveTest
    {
        private CarBrandSaveService _service;
        private FakeBaseRepository<CarBrand> _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new  FakeBaseRepository<CarBrand>();
            this._service = new CarBrandSaveService(_repository);
        }

        [Test]

        public async Task SaveSucess()
        {
            var carBrand = new CarBrand()
            {
                Name= "test-save"
            };
            Exception exception = null;
            try
            {
                await _service.Execute(carBrand);
            }catch(Exception e)
            {
                exception = e;
            }

            Assert.AreEqual(exception, null);

        }

        [Test]

        public async Task NotSaveNameExist()
        {
            var carBrand = new CarBrand()
            {
                Name= "test-save"
            };
            await _repository.Add(carBrand);
            Exception exception = null;
            try
            {
                await _service.Execute(carBrand);
            }catch(RegisterExistException e)
            {
                exception = e;
            }

            Assert.AreNotEqual(exception, null);
            
        }
        
    }
}