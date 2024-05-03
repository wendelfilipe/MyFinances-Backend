using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Application.Interfaces;
using Backend.Domain.Entites.Enums;
using Backend.Domain.Interfaces;

namespace Backend.Application.Services.EntityServices
{
    public class EntityService<U, T> : IEntityService<T> where U : class where T : class
    {
        private readonly IEntityRepository<U> entityRepository;

        private readonly IMapper mapper;
        public EntityService(IEntityRepository<U> entityRepository, IMapper mapper) 
        {
            this.entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));;
            this.mapper = mapper;
        }
        public async Task CreateAsync(T entityDTO)
        {
            var entity = mapper.Map<U>(entityDTO);
            await entityRepository.CreateAsync(entity);
        }

        public async Task DeleteAsync(T entityDTO)
        {
            var entity = mapper.Map<U>(entityDTO);
            await entityRepository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entity = await entityRepository.GetAllAsync();
            return mapper.Map<IEnumerable<T>>(entity);
        }

        public async Task<T> GetByIdAsync(int id)
        {
           var entity = await entityRepository.GetByIdAsync(id);
           return mapper.Map<T>(entity);

        }

        public async Task UpdateAsync(T entityDTO)
        {
            var entity = mapper.Map<U>(entityDTO);
            await entityRepository.UpdateAsync(entity);
        }
    }
}