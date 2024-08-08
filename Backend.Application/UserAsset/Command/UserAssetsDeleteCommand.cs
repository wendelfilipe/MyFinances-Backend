using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Application.UserAsset.Command
{
    public class UserAssetsDeleteCommand : UserAssetsCommand
    {
        public int Id { get; set; }
        public UserAssetsDeleteCommand(int id)
        {
            Id = id;
        }
    }
}