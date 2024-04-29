using Backend.Domain.Entites.WalletEntites;
using Backend.Domain.Interfaces.WalletInterface;
using Backend.Infra.Data.Context;
using Backend.Infra.Data.Reporitories.EntityRepository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infra.Data.EnititesConfiguration
{
    public class WalletRepository : EntityRepository<Wallet>, IWalletRepository
    {
        private readonly AppDbContext context;
        public WalletRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Wallet>> GetAllWalletsByUserId(int userId)
        {
            return await context.Wallets.Where(w => w.UserId == userId).ToListAsync();
        }
    }
}