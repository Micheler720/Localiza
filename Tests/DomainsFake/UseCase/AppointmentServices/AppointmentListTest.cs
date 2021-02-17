using System.Threading.Tasks;
using Domain.UseCase.AppointmentService;
using Domain.Entities;
using Infra.Database.Fake;
using NUnit.Framework;

namespace DomainsFake.UseCase.AppointmentServices
{
    public class AppointmentListTest
    {
        private AppointmentListService _service;
        private FakeAppointmentRepository _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new FakeAppointmentRepository();
            this._service = new AppointmentListService(_repository);

        }
        [Test]
        public async Task ListAppointmentView()
        {
            await this._repository.Add( new Appointment(){
                Id = 123,             
            });

            var appointmentView = await this._service.Execute();

            Assert.AreEqual(appointmentView[0].Id, 123);
        }
    }
}