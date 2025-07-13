using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenTable.Models.DomainModels;

namespace OpenTable.Models.DataLayer.Configuration
{
    internal class ConfigureRestaurant: IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> entity)
        {
            // seed initial data
            entity.HasData(
                new Restaurant
                {
                    Id = 1,
                    Name = "Metro Grill",
                    CuisineId = "american",
                    PriceId = "$$",
                    OpenHours = "11:00-14:00;17:00-21:00",
                    MetropolisId = 1,
                    Address = "1299 Ogden Rd. Wheaton, IL 60006",
                    Phone = "999-999-9999",
                    LogoPath = "MetroGrill.jpg"
                },
                new Restaurant
                {
                    Id = 2,
                    Name = "Saffron Palace",
                    CuisineId = "american",
                    PriceId = "$$$",
                    OpenHours = "12:00-15:00;18:00-22:00",
                    MetropolisId = 2,
                    Address = "88 Spice Blvd, Edison, NJ 08817",
                    Phone = "888-888-8888",
                    LogoPath = "SaffronPalace.jpg"
                },

                new Restaurant
                {
                    Id = 3,
                    Name = "Ocean's Catch",
                    CuisineId = "french",
                    PriceId = "$$$$",
                    OpenHours = "13:00-22:00",
                    MetropolisId = 1,
                    Address = "456 Bay Street, Miami Beach, FL 33139",
                    Phone = "777-777-7777",
                    LogoPath = "OceansCatch.jpg"
                },

                new Restaurant
                {
                    Id = 4,
                    Name = "Burger Haven",
                    CuisineId = "italian",
                    PriceId = "$",
                    OpenHours = "10:00-23:00",
                    MetropolisId = 2,
                    Address = "2300 Sunset Blvd, Los Angeles, CA 90026",
                    Phone = "666-666-6666",
                    LogoPath = "Burger Haven.jpg"
                },

                new Restaurant
                {
                    Id = 5,
                    Name = "Tokyo Bites",
                    CuisineId = "mexican",
                    PriceId = "$$",
                    OpenHours = "11:30-14:30;17:00-22:00",
                    MetropolisId = 1,
                    Address = "1500 Sakura Lane, San Francisco, CA 94110",
                    Phone = "555-555-5555",
                    LogoPath = "TokyoBites.jpg"
                },

                new Restaurant
                {
                    Id = 6,
                    Name = "Bella Cucina",
                    CuisineId = "chinese",
                    PriceId = "$$$",
                    OpenHours = "12:00-15:00;18:00-23:00",
                    MetropolisId = 2,
                    Address = "77 Olive Avenue, Boston, MA 02108",
                    Phone = "444-444-4444",
                    LogoPath = "BellaCucina.jpg"
                }
            );
        }
    }
}
