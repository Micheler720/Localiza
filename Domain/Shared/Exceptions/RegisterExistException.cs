using System;

namespace Domain.Shared.Exceptions
{
    [Serializable]

    public class RegisterExistException : Exception
    {
        public RegisterExistException(string message) : base (message) { }        
    }
}