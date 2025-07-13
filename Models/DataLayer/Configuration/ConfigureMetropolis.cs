using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenTable.Models.DomainModels;

namespace OpenTable.Models.DataLayer.Configuration
{
    internal class ConfigureMetropolis : IEntityTypeConfiguration<Metropolis>
    {
        public void Configure(EntityTypeBuilder<Metropolis> entity)
        {
            // seed initial data
            entity.HasData(
                new Metropolis { Id = 1, Name = "Chicago" },
                new Metropolis { Id = 2, Name = "San Francisco" }
            );
        }

    }
}
