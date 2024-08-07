

using Backend.Application.Interfaces.IEntityServices;
using Backend.Domain.Entites.Enums;

namespace Backend.Application.Interfaces
{
    public interface IEntityService<T> : ICreateService<T>, IDeteleService<T>, IGetAllService<T>, IGetByIdService<T>, IUpdateService<T> where T : class 
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id); 
        Task CreateAsync(T entityDTO);
        Task UpdateAsync(T entityDTO);
        Task DeleteAsync(T entityDTO);
    }
}