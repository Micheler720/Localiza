using System;
using System.Threading.Tasks;
using Domain.UseCase.CategoryServices;
using Domain.UseCase.CategoryServices.Exceptions;
using Domain.UseCase.ModelServices;
using Domain.Entities;
using Infra.Database.Fake;
using NUnit.Framework;
using Domain.Shared.Exceptions;

namespace DomainsFake.UseCase.CategoryServices
{
    public class CategorySaveTest
    {
        private CategorySaveService _service;
        private FakeBaseRepository<CarCategory> _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new  FakeBaseRepository<CarCategory>();
            this._service = new CategorySaveService(_repository);
        }

        [Test]

        public async Task SaveSucess()
        {
            var carCategory = new CarCategory()
            {
                Name= "test-save"
            };
            Exception exception = null;
            try
            {
                await _service.Execute(carCategory);
            }catch(Exception e)
            {
                exception = e;
            }

            Assert.AreEqual(exception, null);

        }

        [Test]

        public async Task NotSaveNameExist()
        {
            var carCategory = new CarCategory()
            {
                Name= "test-save"
            };
            await _repository.Add(carCategory);
            Exception exception = null;
            try
            {
                await _service.Execute(carCategory);
            }catch(CategoryExistException e)
            {
                exception = e;
            }

            Assert.AreNotEqual(exception, null);
            
        }
        
    }
}