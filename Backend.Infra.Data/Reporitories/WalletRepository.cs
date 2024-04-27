using Backend.Domain.Entites.WalletEntites;
using Backend.Infra.Data.Context;
using Backend.Infra.Data.Reporitories.EntityRepository;

namespace Backend.Infra.Data.EnititesConfiguration
{
    public class WalletRepository : EntityRepository<Wallet>
    {
        public WalletRepository(AppDbContext context) : base(context)
        {
        }
    }
}