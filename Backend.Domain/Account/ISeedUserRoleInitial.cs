using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Domain.Account
{
    public interface ISeedUserRoleInitial
    {
        void SeedUsers();
        void SeedRoles();
    }
}