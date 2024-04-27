using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.Services.EntityService;
using Backend.Domain.Entites.AssetsEntites;
using Backend.Domain.Interfaces;

namespace Backend.Application.Services
{
    public class AssetsService : EntityService<Assets>
    {
        public AssetsService(IEntityRepository<Assets> entityRepository, Mapper mapper) : base(entityRepository, mapper)
        {
        }
    }
}