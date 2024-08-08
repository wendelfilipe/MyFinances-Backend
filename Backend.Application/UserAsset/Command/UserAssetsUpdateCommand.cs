using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Application.Asset.Commands;

namespace Backend.Application.UserAsset.Command
{
    public class UserAssetsUpdateCommand : UserAssetsCommand
    {
        public int Id { get; set; }
    }
}