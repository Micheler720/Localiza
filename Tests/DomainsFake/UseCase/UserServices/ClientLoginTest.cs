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
    public class ClientLoginTest
    {        
        private ClientLoginService _service;
        private FakeClientRepository _repository;

        [SetUp]
        public void Setup()
        {
            this._repository = new FakeClientRepository();
            this._service = new ClientLoginService(_repository);
        }

        
        [Test]
        public async Task ClientLoginSucess()
        {
            var user = new Client()
            {
                Id = 0,
                Name = "user-test",
                Birthay = new DateTime(2020,05,01),
                Cpf= "test-registration",
                Password="123456"
            };
            await _repository.Add(user);
            Exception exception = null;
            ClientJWT clientJWT = null;
            var clientLogin = new ClientLogin ()
            {
                Password = user.Password,
                Cpf = user.Cpf
            };
            try
            {
             clientJWT =  await this._service.Login(clientLogin, new FakeAuthentication());
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
            var clientLogin = new ClientLogin ()
            {
                Password = "not-exist",
                Cpf = "not-exist"
            };
            Exception exception = null;
            try{
                await this._service.Login(clientLogin, new FakeAuthentication());
            }catch(Exception ex)
            {
                exception = ex;
            }


            Assert.AreNotEqual(exception, null);
        }

        [Test]
        public async Task NotLoginPasswordWrong()
        {
            var user = new Client()
            {
                Id = 0,
                Name = "user-test",
                Birthay = new DateTime(2020,05,01),
                Cpf= "test-registration",
                Password="123456"
            };
            await _repository.Add(user);
            Exception exception = null;
            var clientLogin = new ClientLogin ()
            {
                Password = "not-exist",
                Cpf = user.Cpf
            };
            try
            {
              await this._service.Login(clientLogin, new FakeAuthentication());
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }

        
    }
}