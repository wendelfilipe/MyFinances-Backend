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
        public SourceCreate SourceCreate { get; protected set; }
        public DateTime? Deleted_at { get; protected set; } = null;
        public DateTime Created_at { get; protected set; }
        public DateTime Updated_at { get; protected set; } 
    }
}