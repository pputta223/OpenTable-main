using OpenTable.Models.DomainModels;
using OpenTable.Models.ExtensionMethods;
using System.Text.Json.Serialization;
namespace OpenTable.Models.Grid
{
    public class RestaurantGridData : GridData
    {
        // set initial sort field in constructor
        //public RestaurantGridData()
        //{
        //    SortField = nameof(Restaurant.Name);
        //}

        //// sort flags
        //[JsonIgnore]
        //public bool SortByHightoLow =>
        //    SortField.EqualsNoCase(nameof(Restaurant.Name));
        //[JsonIgnore]
        //public bool SortByMetropolis =>
        //    SortField.EqualsNoCase(nameof(Restaurant.Metropolis.Name));
        //[JsonIgnore]
        //public bool SortByPriceRange =>
        //    SortField.EqualsNoCase(nameof(Restaurant.Price.Name));
        public RestaurantGridData()
        {
            SortField = "Name"; // default sorting
        }

        [JsonIgnore]
        public bool SortByName => SortField.EqualsNoCase("Name");

        [JsonIgnore]
        public bool SortByMetropolis => SortField.EqualsNoCase("Metropolis");

        [JsonIgnore]
        public bool SortByPriceRange => SortField.EqualsNoCase("Price");
    }
}
