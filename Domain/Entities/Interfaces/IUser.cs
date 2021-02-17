using System;
using Domain.Entities.Roles;

namespace Domain.Entities.Interfaces
{
    public interface IUser
    {
        
        int Id { get; set; }
        string Name { get; set; }        
        string Password { get; set; }
        UserRole UserRole { get; } 

    }
}