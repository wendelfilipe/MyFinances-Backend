using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.Services.EntityService;
using Backend.Domain.Entites.UserEntites;
using Backend.Domain.Interfaces;

namespace Backend.Application.Services
{
    public class UserService : EntityService<User>
    {
        public UserService(IEntityRepository<User> entityRepository, Mapper mapper) : base(entityRepository, mapper)
        {
        }
    }
}