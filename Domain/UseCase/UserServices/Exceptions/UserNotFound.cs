using System;

namespace Domain.UseCase.UserServices.Exceptions
{
    [Serializable]
    public class UserNotFound: Exception
    {
        public UserNotFound(string message) : base(message) { }
        
    }
}