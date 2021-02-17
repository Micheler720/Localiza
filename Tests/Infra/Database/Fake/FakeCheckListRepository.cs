using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;

namespace Infra.Database.Fake
{
    public class FakeCheckListRepository : FakeBaseRepository<CheckList>, IChecklistRepository<CheckList>
    {
        public async Task<CheckList> FindById(int id)
        {
            await Task.Delay(2000);
            return _data.Where(checkList => checkList.Id == id).FirstOrDefault();
        }
    }
}