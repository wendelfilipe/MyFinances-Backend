using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.DTOs;
using Backend.Application.Interfaces;
using Backend.Application.Services.EntityServices;
using Backend.Domain.Entites.AssetsEntites;
using Backend.Domain.Interfaces;
using Backend.Domain.Interfaces.AssetsInterface;

namespace Backend.Application.Services
{
    public class AssetsService : EntityService<AssetsDTO>, IAssetsService
    {
        public AssetsService(IEntityRepository<AssetsDTO> entityRepository, IMapper mapper, IAssetsRepository assetsRepository) : base(entityRepository, mapper)
        {
        }
    }
}