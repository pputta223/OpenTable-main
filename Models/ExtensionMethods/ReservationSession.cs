namespace OpenTable.Models.ExtensionMethods
{
    public class ReservationSession
    {
        private const string CartKey = "mycart";
        private const string CountKey = "cartcount";
        private const string MetropolisKey = "metropolis";
        private const string PriceKey = "price";
        private const string CuisineKey = "cuisine";

        private ISession session { get; }

        public ReservationSession(ISession session)
        {
            this.session = session;
        }
        public string GetActiveMetropolis() =>
            session.GetString(MetropolisKey) ?? string.Empty;

        public void SetActiveMetropolis(string activeMetropolis) =>
            session.SetString(MetropolisKey, activeMetropolis);

        public string GetActivePrice() =>
            session.GetString(PriceKey) ?? string.Empty;

        public void SetActivePrice(string activePrice) =>
            session.SetString(PriceKey, activePrice);

        public string GetActiveCuisine() =>
            session.GetString(CuisineKey) ?? string.Empty;

        public void SetActiveCuisine(string activeCuisine) =>
            session.SetString(CuisineKey, activeCuisine);
        public int? GetCartCount() =>
            session.GetInt32(CountKey);

        public void SetCartIds(List<int> reservationIds)
        {
            session.SetObject(CartKey, reservationIds);
            session.SetInt32(CountKey, reservationIds.Count);
        }

        public List<int> GetCartIds() =>
            session.GetObject<List<int>>(CartKey) ?? new List<int>();

        public void ClearCart()
        {
            session.Remove(CartKey);
            session.Remove(CountKey);
        }
    }
}