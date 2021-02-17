using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Database.Implementations.SQLServerDriver.Repositories.AppointmentyRepository
{
    public class CheckListRepositorySQLDriver : BaseSQLServerRepository<CheckList>, IChecklistRepository<CheckList>
    {
    }
}
