using Backend.Application.DTOs;
using Backend.Domain.Entites.WalletEntites;

namespace Backend.Application.Interfaces
{
    public interface IWalletService 
    {
        public Task DeteleAsync(int id);
        public Task CreateAsync(WalletDTO walletDTO);
        public Task UpdateAsync(WalletDTO walletDTO);
        public Task<IEnumerable<WalletDTO>> GetAllWalletDTOByUserId(int userId);
    }
}