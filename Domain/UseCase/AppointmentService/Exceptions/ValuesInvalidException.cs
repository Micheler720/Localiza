using System;

namespace Domain.UseCase.AppointmentService.Exceptions
{
    [Serializable]
    public class ValuesInvalidException : Exception
    {
        public ValuesInvalidException(string message) : base(message)
        {
            
        }
    }
}