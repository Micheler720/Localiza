using System;

namespace Domain.UseCase.CarServices.Exceptions
{
    [Serializable]

    public class CarExistException : Exception
    {
        public CarExistException(string message) : base (message) { }        
    }
}