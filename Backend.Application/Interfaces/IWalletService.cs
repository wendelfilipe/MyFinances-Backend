using Backend.Application.DTOs;

namespace Backend.Application.Interfaces
{
    public interface IWalletService : IEntityService<WalletDTO>
    {
        public Task<IEnumerable<WalletDTO>> GetAllWalletDTOByUserId(int userId);
    }
}