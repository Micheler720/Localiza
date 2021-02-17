using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using Domain.Shared.Exceptions;

namespace Domain.UseCase.ModelServices
{
    public class CarModelDeleteService
    {
        private IBaseRepository<CarModel> _repository;
        
        public CarModelDeleteService(IBaseRepository<CarModel> repository)
        {
            this._repository = repository;
        }

        public async Task Execute (int id)
        {
            if(id == 0) throw new NotFoundRegisterException("Marca não Encontrada não encontrado.");
            var register = await this._repository.Filter(c => c.Id == id);
            if(register == null) throw new NotFoundRegisterException("Marca não Encontrada  não encontrado.");
            await _repository.Delete(register[0]);
        }
    }
}