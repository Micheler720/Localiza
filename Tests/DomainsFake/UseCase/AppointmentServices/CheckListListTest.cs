using System.Threading.Tasks;
using Domain.UseCase.AppointmentService;
using Domain.Entities;
using Infra.Database.Fake;
using NUnit.Framework;

namespace DomainsFake.UseCase.AppointmentServices
{
    public class CheckListListTest
    {
        private ChekListListService _service;
        private FakeCheckListRepository _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new FakeCheckListRepository();
            this._service = new ChekListListService(_repository);
        }   

        [Test]
        public async Task ListCheckList()
        {
            await this._repository.Add( new CheckList(){
                Id = 123,             
            });

            var checklistView = await this._service.Execute();

            Assert.AreEqual(checklistView[0].Id, 123);
        }
    }
}