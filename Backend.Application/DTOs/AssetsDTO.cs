using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.Enums;

namespace Backend.Application.DTOs
{
    public class AssetsDTO
    {
         public int Id { get;  set; }
        public int WalletId {get;  set; }
        
        [Required(ErrorMessage = "The Code Name is required")]
        [MaxLength(10)]
        [MinLength(5)]
        public string CodName { get;  set; }

        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "The current price is required")]
        public decimal CurrentPrice { get;  set; }

        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "The buy price is required")]
        public decimal BuyPrice { get; set; }
        public long Amount { get; set; }
        public SourceTypeAssets SourceTypeAssets {get; set; }
        public decimal AveregePrice { get;  set; } = 0;
        public SourceCreate SourceCreate { get;  set; }
        public DateTime? Deleted_at { get;  set; } = null;
        public DateTime Created_at { get;  set; } = DateTime.UtcNow;
        public DateTime Updated_at { get;  set; } = DateTime.UtcNow;

    }
}