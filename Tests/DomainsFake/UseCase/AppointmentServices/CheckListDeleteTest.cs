using System;
using System.Threading.Tasks;
using Domain.UseCase.AppointmentService;
using Domain.Entities;
using Infra.Database.Fake;
using NUnit.Framework;
using Domain.Shared.Exceptions;

namespace DomainsFake.UseCase.AppointmentServices
{
    public class CheckListDeleteTest
    {
        private CheckListDeleteService _service;
        private FakeCheckListRepository _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new FakeCheckListRepository();
            this._service = new CheckListDeleteService(_repository);

        }

        [Test]
        public async Task DeleteCheckListSucess()
        {
            var checkList = new CheckList()
            {
                Id = 1                
            };
            await _repository.Add(checkList);
            Exception exception = null;
            try{
                await _service.Execute(1);
            }catch(Exception ex)
            {
                exception = ex;
            }

            Assert.AreEqual(exception, null);
        }

         [Test]
        public async Task NotDeleteCheckListIdZero()
        {
            var checkList = new CheckList()
            {
                Id = 0               
            };
            await _repository.Add(checkList);
            Exception exception = null;
            try{
                await _service.Execute(0);
            }catch(NotFoundRegisterException ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }

        [Test]
        public async Task NotDeleteCheckNotRegister()
        {
            
            Exception exception = null;
            try{
                await _service.Execute(1);
            }catch(NotFoundRegisterException ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }
    }
}