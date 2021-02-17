using System.Threading.Tasks;
using Domain.UseCase.CarServices;
using Domain.Entities;
using Infra.Database.Fake;
using NUnit.Framework;

namespace DomainsFake.UseCase.CarServices
{
    public class CarListTest
    {
        
        private CarListService _service;
        private FakeCarRepository _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new  FakeCarRepository();
            this._service = new CarListService(_repository);
        }

        [Test]
        public async Task ListModelCar ()
        {
            
            await this._repository.Add( new Car(){
                Id = 123,             
            });

            var modelCarList = await this._service.Execute();

            Assert.AreEqual(modelCarList[0].Id, 123);
        }
    }
}