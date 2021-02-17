using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Shared.Exceptions
{
    [Serializable]
    public class NotFoundParameterException : Exception
    {
        public NotFoundParameterException(string message) : base(message) { }
    }
}
