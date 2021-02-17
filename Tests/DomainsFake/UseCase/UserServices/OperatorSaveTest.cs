using System;
using System.Threading.Tasks;
using Domain.UseCase.UserServices;
using Domain.UseCase.UserServices.Exceptions;
using Domain.Entities;
using Domain.Entities.Roles;
using Infra.Database.Fake;
using NUnit.Framework;

namespace DomainsFake.UseCase.UserServices
{
    public class OperatorSaveTest
    {        
        private OperatorSaveService _service;
        private FakeOperatorRepository _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new FakeOperatorRepository();
            this._service = new OperatorSaveService(_repository);
        }

        
        [Test]
        public async Task AddUserSucess()
        {
            var user = new Operator()
            {
                Id = 0,
                Name = "user-test",
                Registration= "test-registration",
                Password = "123456"
            };
            Exception exception = null;
            try
            {
              await this._service.Execute(user);
            }
            catch (UserNotDefinid ex)
            {
                exception = ex;
            }

            Assert.AreEqual(exception, null);
        }
        
        [Test]
        public async Task TestUserPerson()
        {
            var user = new Operator()
            {
                Id = 0,
                Name = "user-test",
                Registration= "test-registration",
                Password = "123456"
            };

            await this._repository.Add(user);

            await this._service.Execute(user);
            var userRegister = await this._repository.GetAll();

            Assert.AreEqual(userRegister[0].Registration, "test-registration");
            Assert.AreEqual(userRegister[0].UserRole, UserRole.Operator);
        }       

        
        [Test]
        public async Task NotRegisterUserAlreadyRegister()
        {
            var user = new Operator()
            {
                Id = 0,
                Name = "user-test",
            };

            Exception exception = null;

            try{
                await this._service.Execute(user);

            }catch(UserNotDefinid ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }
    }
}