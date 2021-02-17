using NUnit.Framework;
using Domain.UseCase.UserServices;
using Infra.Database.Fake;
using Domain.Entities;
using System;
using System.Threading.Tasks;
using Domain.UseCase.UserServices.Exceptions;

namespace DomainsFake.UseCase.UserServices
{
    public class OperatorDeleteTest
    {
        private OperatorDeleteService _service;
        private FakeOperatorRepository _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new FakeOperatorRepository();
            this._service = new OperatorDeleteService(_repository);

        }
        [Test]
        public async Task DeleteUserSucess()
        {
            var user = new Operator()
            {
                Id = 123,
                Name = "user-test",
                Registration = "123"
            };
            await this._repository.Add(user);
            Exception exception = null;
            try
            {
              await this._service.Execute(123);
            }
            catch (UserNotFound ex)
            {
                exception = ex;
            }

            Assert.AreEqual(exception, null);
        }

        [Test]
        public async Task NotDeleteUserIdZero()
        {
            var user = new Operator()
            {
                Id = 0,
                Name = "user-test",
                Registration = "123"
            };
            Exception exception = null;
            try
            {
              await this._service.Execute(123);
            }
            catch (UserNotFound ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }
        public async Task NotDeleteUserRegisterNot()
        {
            var user = new Operator()
            {
                Id = 123,
                Name = "user-test",
                Registration = "123"
            };
            Exception exception = null;
            try
            {
              await this._service.Execute(123);
            }
            catch (UserNotFound ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }
    }
}