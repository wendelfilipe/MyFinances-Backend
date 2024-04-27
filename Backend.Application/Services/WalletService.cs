using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.Services.EntityService;
using Backend.Domain.Entites.WalletEntites;
using Backend.Domain.Interfaces;

namespace Backend.Application.Services
{
    public class WalletService : EntityService<Wallet>
    {
        public WalletService(IEntityRepository<Wallet> entityRepository, Mapper mapper) : base(entityRepository, mapper)
        {
        }
    }
}