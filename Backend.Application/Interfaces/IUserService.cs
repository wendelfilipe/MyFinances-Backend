using Backend.Application.DTOs;
using Backend.Application.Interfaces.IEntityServices;
using Backend.Domain.Entites.UserEntites;

namespace Backend.Application.Interfaces
{
    public interface IUserService : ICreateService<UserDTO>, IUpdateService<UserDTO>, IGetByIdService<UserDTO>
    {
        Task<UserDTO> GetUserDTOByEmailAsync(string email);
    }
}