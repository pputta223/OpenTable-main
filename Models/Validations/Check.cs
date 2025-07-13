using OpenTable.Models.DataLayer;

namespace OpenTable.Models.Validations
{
    public static class Check
    {
        public static string EmailExists(OpenTableContext ctx, string email)
        {
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                var customer = ctx.AppUsers.FirstOrDefault(
                    c => c.Email.ToLower() == email.ToLower());
                if (customer != null)
                    msg = $"Email address {email} already in use.";
            }
            return msg;
        }
    }
}