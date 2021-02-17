using System;
using System.Threading.Tasks;
using Domain.UseCase.UserServices;
using Domain.UseCase.UserServices.Exceptions;
using Domain.Entities;
using Infra.Authentication;
using Infra.Database.Fake;
using NUnit.Framework;
using Domain.ViewModel.Users;

namespace DomainsFake.UseCase.UserServices
{
    public class OperatorLoginTest
    {        
        private OperatorLoginService _service;
        private FakeOperatorRepository _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new FakeOperatorRepository();
            this._service = new OperatorLoginService(_repository);
        }

        
        [Test]
        public async Task OperatorLoginSucess()
        {
            var user = new Operator()
            {
                Id = 0,
                Name = "user-test",
                Registration = "test-registration",
                Password="123456"
            };
            await _repository.Add(user);
            Exception exception = null;
            OperatorJWT clientJWT = null;
            var operatorLogin = new OperatorLogin ()
            {
                Password = user.Password,
                Resgistration = user.Registration
            };
            try
            {
             clientJWT =  await this._service.Login(operatorLogin, new FakeAuthentication());
            }
            catch (UserNotDefinid ex)
            {
                exception = ex;
            }

            Assert.AreEqual(exception, null);
            Assert.AreEqual(clientJWT.Token, "token-implementation-fake");
        }
        
        [Test]
        public async Task NotLoginUserNotExist()
        {
            var operatorLogin = new OperatorLogin ()
            {
                Password = "not-exist",
                Resgistration = "not-exist"
            };
            Exception exception = null;
            try{
                await this._service.Login(operatorLogin, new FakeAuthentication());
            }catch(Exception ex)
            {
                exception = ex;
            }


            Assert.AreNotEqual(exception, null);
        }

        [Test]
        public async Task NotLoginPasswordWrong()
        {
            var user = new Operator()
            {
                Id = 0,
                Name = "user-test",
                Registration= "test-registration",
                Password="123456"
            };
            await _repository.Add(user);
            Exception exception = null;
            var operatorLogin = new OperatorLogin ()
            {
                Password = "not-exist",
                Resgistration = user.Registration
            };
            try
            {
              await this._service.Login(operatorLogin, new FakeAuthentication());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }

        
    }
}