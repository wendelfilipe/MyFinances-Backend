using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Application.Interfaces.IEntityServices
{
    public interface IUpdateService<T> where T : class
    {
        Task UpdateAsync(T entityDTO);
    }
}