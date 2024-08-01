using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Application.Assets.Commands
{
    public class AssetsEntityUpdateCommand : AssetsEntityCommand
    {
        public int Id { get; set; }
        
    }
}