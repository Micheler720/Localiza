using System.Threading.Tasks;
using Domain.UseCase.BrandServices;
using Domain.UseCase.ModelServices;
using Domain.Entities;
using Infra.Database.Fake;
using NUnit.Framework;

namespace DomainsFake.UseCase.BrandService
{
    public class BrandListTest
    {
        
        private CarBrandListService _service;
        private FakeBaseRepository<CarBrand> _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new  FakeBaseRepository<CarBrand>();
            this._service = new CarBrandListService(_repository);
        }

        [Test]
        public async Task ListBrandCar ()
        {
            
            await this._repository.Add( new CarBrand(){
                Id = 123,             
            });

            var modelCarList = await this._service.Execute();

            Assert.AreEqual(modelCarList[0].Id, 123);
        }
    }
}