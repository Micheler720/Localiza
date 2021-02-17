using System;

namespace Domain.UseCase.CategoryServices.Exceptions
{
    [Serializable]

    public class CategoryExistException : Exception
    {
        public CategoryExistException(string message) : base (message) { }        
    }
}