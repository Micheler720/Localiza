using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Interfaces
{
    public interface IRegister
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
