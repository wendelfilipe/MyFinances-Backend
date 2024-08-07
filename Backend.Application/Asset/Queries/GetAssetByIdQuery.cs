using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.AssetsEntites;
using MediatR;

namespace Backend.Application.Asset.Queries
{
    public class GetAssetByIdQuery : IRequest<Assets>
    {
        public int Id { get; set; }
        public GetAssetByIdQuery(int id)
        {
            Id = id;          
        }
    }
}