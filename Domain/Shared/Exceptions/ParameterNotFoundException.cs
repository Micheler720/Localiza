using System;

namespace Domain.Shared.Exceptions
{
    public class ParameterNotFoundException: Exception
    {
        public ParameterNotFoundException(string message): base(message)
        {
             
        }
    }
}
