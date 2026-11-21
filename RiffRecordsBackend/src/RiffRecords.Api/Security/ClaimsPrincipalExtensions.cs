using System.Globalization;
using System.Security.Claims;

namespace RiffRecords.Api.Security
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var sub = user.FindFirstValue("sub")
                ?? throw new UnauthorizedAccessException("Token has no 'sub' claim.");

            return int.Parse(sub, CultureInfo.InvariantCulture);
        }
    }
}
