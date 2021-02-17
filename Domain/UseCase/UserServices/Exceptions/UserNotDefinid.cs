using System;

namespace Domain.UseCase.UserServices.Exceptions
{
    [Serializable]
    public class UserNotDefinid: Exception
    {
        public UserNotDefinid(string message) : base(message) { }
        
    }
}