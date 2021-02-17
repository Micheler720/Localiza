using System;

namespace Domain.UseCase.AppointmentService.Exceptions
{
    [Serializable]
    public class AppointmentNotExcludeException : Exception
    {
        public AppointmentNotExcludeException(string message) : base(message)
        {
            
        }
    }
}