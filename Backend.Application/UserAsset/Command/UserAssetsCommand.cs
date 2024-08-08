using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.Enums;
using Backend.Domain.Entites.UserAssetsEntity;
using MediatR;

namespace Backend.Application.UserAsset.Command
{
    public class UserAssetsCommand : IRequest<UserAssets>
    {
        public int WalletId { get; set; }
        public int AssetsId { get; set; }
        public decimal? PerCentCDI { get; set; }
        public long Amount { get; set; }
        public decimal BuyPrice { get;  set; }
        public decimal AveregePrice { get; set; }
        public SourceTypeAssets SourceTypeAssets {get; set; }
        public SourceCreate SourceCreate { get; set; }
        public string StartDate { get; private set; }
        public string? EndDate { get; private set; }
        public DateTime? Deleted_at { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; } 
    }
}