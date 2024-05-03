

using Backend.Domain.Entites.Enums;

namespace Backend.Application.Interfaces
{
    public interface IEntityService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entityDTO);
        Task UpdateAsync(T entityDTO);
        Task DeleteAsync(T entityDTO);
    }
}