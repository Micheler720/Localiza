using System;

namespace Domain.UseCase.UserServices.Exceptions
{
    [Serializable]
    public class CarNotFoundException: Exception
    {
        public CarNotFoundException(string message) : base(message) { }
        
    }
}