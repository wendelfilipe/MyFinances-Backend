using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.Enums;

namespace Backend.Domain.Entites
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public SourceAssets source { get; protected set; }
    }
}