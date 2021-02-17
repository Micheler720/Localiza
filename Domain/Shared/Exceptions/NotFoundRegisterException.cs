using System;

namespace Domain.Shared.Exceptions
{
    [Serializable]
    public class NotFoundRegisterException: Exception
    {
        public NotFoundRegisterException(string message) : base(message) { }
        
    }
}