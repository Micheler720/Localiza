using System.Threading.Tasks;
using Domain.UseCase.ModelServices;
using Domain.Entities;
using Infra.Database.Fake;
using NUnit.Framework;

namespace DomainsFake.UseCase.ModelService
{
    public class CarModelListTest
    {
        
        private CarModelListService _service;
        private FakeBaseRepository<CarModel> _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new  FakeBaseRepository<CarModel>();
            this._service = new CarModelListService(_repository);
        }

        [Test]
        public async Task ListModelCar ()
        {
            
            await this._repository.Add( new CarModel(){
                Id = 123,             
            });

            var modelCarList = await this._service.Execute();

            Assert.AreEqual(modelCarList[0].Id, 123);
        }
    }
}