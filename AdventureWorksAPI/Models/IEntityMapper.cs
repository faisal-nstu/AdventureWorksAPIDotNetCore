using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AdventureWorksAPI.Models
{
    public interface IEntityMapper
    {
        IEnumerable<IEntityMap> Mappings { get; }

        void MapEntities(ModelBuilder modelBuilder);
    }
}
