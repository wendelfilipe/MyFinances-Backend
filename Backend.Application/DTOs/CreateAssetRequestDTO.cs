using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.Enums;

namespace Backend.Application.DTOs
{
    public class CreateAssetRequestDTO
    {
         public int Id { get; set; }
        public int WalletId { get;  set; }
        public int AssetsId { get;  set; }
        public string CodName { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal? PerCentCDI { get;  set; }
        public long Amount { get;  set; }
        public decimal BuyPrice { get;   set; }
        public decimal AveregePrice { get;  set; }
        public SourceCreate SourceCreate { get; set; }
        public SourceTypeAssets SourceTypeAssets { get; set; }
        public string StartDate { get; set; }
        public string? EndDate { get; set; }
        public DateTime? Deleted_at { get; set; } = null;
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; } 
    }
}