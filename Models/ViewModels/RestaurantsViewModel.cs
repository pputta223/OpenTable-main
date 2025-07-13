using OpenTable.Models.DomainModels;
using OpenTable.Models.Grid;

namespace OpenTable.Models.ViewModels;

public class RestaurantsViewModel
{
    public string ActiveMetropolis { get; set; } = "all";
    public string ActivePriceRange { get; set; } = "all";
    public string ActiveCuisine { get; set; } = "all";
    public IEnumerable<Restaurant> Restaurantss { get; set; } = new List<Restaurant>();
    public RestaurantGridData CurrentRoute { get; set; } = new RestaurantGridData();
    public int TotalPages { get; set; }

    public Restaurant Restaurant { get; set; } = null!;

    public List<Restaurant> Restaurants { get; set; } = null!;
    public List<Reservation> Reservations { get; set; } = null!;

    public List<Metropolis> Metropolises { get; set; } = new List<Metropolis>();
    public List<Price> PriceRanges { get; set; } = new List<Price>();
    public List<Cuisine> Cuisines { get; set; } = new List<Cuisine>();

    // methods to help view determine active link
    public string CheckActiveMetropolis(string c) =>
        c.ToLower() == ActiveMetropolis.ToLower() ? "active" : "";

    public string CheckActivePriceRange(string d) =>
        d.ToLower() == ActivePriceRange.ToLower() ? "active" : "";

    public string CheckActiveCuisine(string d) =>
        d.ToLower() == ActiveCuisine.ToLower() ? "active" : "";
}