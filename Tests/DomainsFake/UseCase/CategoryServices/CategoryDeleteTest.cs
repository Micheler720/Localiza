using System;
using System.Threading.Tasks;
using Domain.UseCase.CategoryServices;
using Domain.UseCase.ModelServices;
using Domain.Entities;
using Infra.Database.Fake;
using NUnit.Framework;
using Domain.Shared.Exceptions;

namespace DomainsFake.UseCase.CategoryServices
{
    public class CategoryDeleteTest
    {
        private CategoryDeleteService _service;
        private FakeBaseRepository<CarCategory> _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new  FakeBaseRepository<CarCategory>();
            this._service = new CategoryDeleteService(_repository);
        }

        [Test]
        public async Task DeleteModelSucess ()
        {
            var model = new CarCategory()
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