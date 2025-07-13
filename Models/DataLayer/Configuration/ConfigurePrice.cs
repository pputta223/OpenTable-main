using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenTable.Models.DomainModels;

namespace OpenTable.Models.DataLayer.Configuration
{
    internal class ConfigurePrice : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> entity)
        {
            // seed initial data
            entity.HasData(
                new Price { PriceId = "$", Name = "$" },
                new Price { PriceId = "$$", Name = "$$" },
                new Price { PriceId = "$$$", Name = "$$$" },
                new Price { PriceId = "$$$$", Name = "$$$$" }
            );
        }

    }
}
