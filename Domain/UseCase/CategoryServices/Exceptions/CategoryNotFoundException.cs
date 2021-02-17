using System;

namespace Domain.UseCase.CategoryServices.Exceptions
{
    [Serializable]
    public class CategoryNotFoundException: Exception
    {
        public CategoryNotFoundException(string message) : base(message) { }
        
    }
}