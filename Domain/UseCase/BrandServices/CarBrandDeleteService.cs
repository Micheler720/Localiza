using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using Domain.Shared.Exceptions;

namespace Domain.UseCase.BrandServices
{
    public class CarBrandDeleteService
    {
        private IBaseRepository<CarBrand> _repository;
        
        public CarBrandDeleteService(IBaseRepository<CarBrand> repository)
        {
            this._repository = repository;
        }

        public async Task Execute (int id)
        {
            if(id == 0) throw new NotFoundRegisterException("Modelo não encontrado.");
            var register = await _repository.FindById(id);
            if(register == null) throw new NotFoundRegisterException("Modelo não encontrado.");
            await _repository.Delete(register);
        }
    }
}