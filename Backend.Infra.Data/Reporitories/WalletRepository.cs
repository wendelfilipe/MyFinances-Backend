using Backend.Domain.Entites.WalletEntites;
using Backend.Domain.Interfaces.WalletInterface;
using Backend.Infra.Data.Context;
using Backend.Infra.Data.Reporitories.EntityRepository;

namespace Backend.Infra.Data.EnititesConfiguration
{
    public class WalletRepository : EntityRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(AppDbContext context) : base(context)
        {
        }
    }
}