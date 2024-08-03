using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.Asset.Commands;

namespace Backend.Application.Asset.Commands
{
    public class AssetsRemoveCommand : AssetsCommand
    {
        public int Id { get; set; }
        public AssetsRemoveCommand(int id)
        {
            Id = id;
        }
    }
}