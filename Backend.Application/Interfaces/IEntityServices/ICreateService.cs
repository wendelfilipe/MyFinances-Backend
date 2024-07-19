using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Application.Interfaces.IEntityServices
{
    public interface ICreateService<T> where T : class
    {
        Task CreateAsync(T entityDTO);
    }
}