using System;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Domain.Repositories;

namespace Infra.Database.Implementations.EntityFramework.Repositories.AppointmentRepository
{
    public class ChecklistRepositoryEntity  : BaseEntityRepository<CheckList>, IChecklistRepository<CheckList>
    {
        public ChecklistRepositoryEntity(ContextEntity context) : base(context)
        {
            
        }

    }
}