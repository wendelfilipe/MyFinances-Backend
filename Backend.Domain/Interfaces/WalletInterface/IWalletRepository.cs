using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.WalletEntites;

namespace Backend.Domain.Interfaces.WalletInterface
{
    public interface IWalletRepository : IEntityRepository<Wallet>
    {
        public Task<IEnumerable<Wallet>> GetAllWalletsByUserId(int userId);
    }
}