namespace OpenTable.Models.Utils;

public class Filters
{

    public Filters(string filterstring)
    {
        FilterString = filterstring ?? "all-all-all";
        string[] filters = FilterString.Split('-');
        MetropolisId = filters[0].ToLower() == "all" ? null : int.Parse(filters[0]);
        PriceId = filters[1];
        CuisineId = filters[2];
    }
    public string FilterString { get; }
    public int? MetropolisId { get; }
    public string PriceId { get; }
    public string CuisineId { get; }

    public bool HasMetropolis => MetropolisId.HasValue;
    public bool HasPrice => PriceId.ToLower() != "all";
    public bool HasCuisine => CuisineId.ToLower() != "all";
}
