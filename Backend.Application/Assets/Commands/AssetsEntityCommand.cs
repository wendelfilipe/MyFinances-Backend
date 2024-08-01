using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites;
using Backend.Domain.Entites.Enums;
using MediatR;

namespace Backend.Application.Assets.Commands
{
    public class AssetsEntityCommand : IRequest<AssetsEntity>
    {
        public string CodName { get; set; }
        public decimal CurrentPrice { get; set; }
        public SourceTypeAssets SourceTypeAssets {get; set; }
        public SourceCreate SourceCreate { get; set; }
        public DateTime? Deleted_at { get; set; } = null;
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}