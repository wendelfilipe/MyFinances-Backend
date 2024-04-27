using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.DTOs;
using Backend.Application.Services.EntityServices;
using Backend.Domain.Entites.WalletEntites;
using Backend.Domain.Interfaces;

namespace Backend.Application.Services
{
    public class WalletService : EntityService<WalletDTO>
    {
        public WalletService(IEntityRepository<WalletDTO> entityRepository, IMapper mapper) : base(entityRepository, mapper)
        {
        }
    }
}