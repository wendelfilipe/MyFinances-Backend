using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Application.Interfaces.IEntityServices
{
    public interface IGetByIdService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
    }
}