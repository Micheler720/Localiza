using System;

namespace Domain.UseCase.UserServices.Exceptions
{
    [Serializable]
    public class UniqUserRegisterCpf : Exception
    {
        public UniqUserRegisterCpf(string message) : base(message) { }
    }
}