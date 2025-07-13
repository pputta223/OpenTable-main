namespace OpenTable.Models.ExtensionMethods
{
    public class ReservationCookies
    {
        private const string CartKey = "reservationcart";
        private const string Delimiter = "|";

        private IRequestCookieCollection requestCookies;
        private IResponseCookies responseCookies;

        public ReservationCookies(IRequestCookieCollection cookies)
        {
            requestCookies = cookies;
            responseCookies = null!;
        }

        public ReservationCookies(IResponseCookies cookies)
        {
            responseCookies = cookies;
            requestCookies = null!;
        }

        public void SetReservationIds(List<int> reservationIds)
        {
            string value = string.Join(Delimiter, reservationIds);

            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            };

            RemoveReservationKeys(); // clear old cookie first
            responseCookies.Append(CartKey, value, options);
        }
        public List<int> GetReservationIds()
        {
            var cookie = requestCookies[CartKey];

            if (string.IsNullOrEmpty(cookie))
                return new List<int>();

            return cookie
                .Split(Delimiter, StringSplitOptions.RemoveEmptyEntries)
                .Select(id => int.TryParse(id, out var parsed) ? parsed : (int?)null)
                .Where(id => id.HasValue)
                .Select(id => id!.Value)
                .ToList();
        }

        public void RemoveReservationKeys()
        {
            responseCookies.Delete(CartKey);
        }
    }
}