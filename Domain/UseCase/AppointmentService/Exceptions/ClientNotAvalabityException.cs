using System;

namespace Domain.UseCase.AppointmentService.Exceptions
{
    [Serializable]
    public class ClientNotAvalabityException : Exception
    {
         public ClientNotAvalabityException(string message) : base(message) { }
    }
}