using System.Threading.Tasks;
using Domain.UseCase.UserServices;
using Domain.Entities;
using Infra.Database.Fake;
using NUnit.Framework;

namespace DomainsFake.UseCase.UserServices
{
    public class OperatorListTest
    {
        private OperatorListService _service;
        private FakeOperatorRepository _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new FakeOperatorRepository();
            this._service = new OperatorListService(_repository);
        }

        [Test]
        public async Task ListUserView()
        {
            await this._repository.Add( new Operator(){
                Id = 123,
                Name = "test-user",
                Registration ="123154", 
                Password = "123456",               
            });

            var userView = await this._service.Execute();

            Assert.AreEqual(userView[0].Name, "test-user");
            Assert.AreEqual(userView[0].Registration, "123154");
        }
    }
}