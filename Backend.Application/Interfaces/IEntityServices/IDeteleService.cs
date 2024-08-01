using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Application.Interfaces.IEntityServices
{
    public interface IDeteleService<T> where T : class
    {
        Task DeleteAsync(T entityDTO);
    }
}