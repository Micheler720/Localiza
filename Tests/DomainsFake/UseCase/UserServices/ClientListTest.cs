using System.Threading.Tasks;
using Domain.UseCase.UserServices;
using Domain.Entities;
using Infra.Database.Fake;
using NUnit.Framework;

namespace DomainsFake.UseCase.UserServices
{
    public class ClientListTest
    {
        private ClientListService _service;
        private FakeClientRepository _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new FakeClientRepository();
            this._service = new ClientListService(_repository);
        }

        [Test]
        public async Task ListUserView()
        {
            await this._repository.Add( new Client(){
                Id = 123,
                Name = "test-user",
                Cpf="123154", 
                Password = "123456",               
            });

            var userView = await this._service.Execute();

            Assert.AreEqual(userView[0].Name, "test-user");
            Assert.AreEqual(userView[0].Cpf, "123154");
        }
    }
}