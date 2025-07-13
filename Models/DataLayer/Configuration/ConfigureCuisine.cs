using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenTable.Models.DomainModels;

namespace OpenTable.Models.DataLayer.Configuration
{
    internal class ConfigureCuisine : IEntityTypeConfiguration<Cuisine>
    {
        public void Configure(EntityTypeBuilder<Cuisine> entity)
        {
            // seed initial data
            entity.HasData(
                  new Cuisine { CuisineId = "american", Name = "American" },
                new Cuisine { CuisineId = "french", Name = "French" },
                new Cuisine { CuisineId = "italian", Name = "Italian" },
                new Cuisine { CuisineId = "mexican", Name = "Mexican" },
                new Cuisine { CuisineId = "chinese", Name = "Chinese" },
                new Cuisine { CuisineId = "japanese", Name = "Japanese" },
                new Cuisine { CuisineId = "indian", Name = "Indian" },
                new Cuisine { CuisineId = "mediterranean", Name = "Mediterranean" }
            );
        }

    }
}
