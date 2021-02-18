using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Interfaces
{
    interface IAdress
    {
        string CEP { get; set; }
        string Logradouro { get; set; }
        int Number { get; set; }
        string City { get; set; }
        string Complement { get; set; }
        string State { get; set; }
    }
}
