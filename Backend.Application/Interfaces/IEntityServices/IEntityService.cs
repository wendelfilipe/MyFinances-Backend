

using Backend.Application.Interfaces.IEntityServices;
using Backend.Domain.Entites.Enums;

namespace Backend.Application.Interfaces
{
    public interface IEntityService<T> : ICreateService<T>, IDeteleService<T>, IGetAllService<T>, IGetByIdService<T>, IUpdateService<T> where T : class 
    {
    }
}