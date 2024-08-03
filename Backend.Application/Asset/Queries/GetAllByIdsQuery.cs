using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.AssetsEntites;
using MediatR;

namespace Backend.Application.Asset.Queries
{
    public class GetAllByIdsQuery : IRequest<IEnumerable<Assets>>
    {
        public IEnumerable<int>? Ids { get; set; }
        public GetAllByIdsQuery(IEnumerable<int>? ids)
        {
            Ids = ids;
        }
    }
}