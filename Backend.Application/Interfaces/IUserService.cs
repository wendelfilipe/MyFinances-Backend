using Backend.Application.DTOs;
using Backend.Domain.Entites.UserEntites;

namespace Backend.Application.Interfaces
{
    public interface IUserService 
    {
        Task<UserDTO> GetUserDTOByEmailAsync(string email);
    }
}