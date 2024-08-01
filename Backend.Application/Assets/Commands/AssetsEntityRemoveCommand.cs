using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Application.Assets.Commands
{
    public class AssetsEntityRemoveCommand : AssetsEntityCommand
    {
        public int Id { get; set; }
        public AssetsEntityRemoveCommand(int id)
        {
            Id = id;
        }
    }
}