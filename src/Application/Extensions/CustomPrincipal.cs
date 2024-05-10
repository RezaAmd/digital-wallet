using System.Security.Claims;

namespace DigitalWallet.Application.Extensions
{
    public static class CustomPrincipal
    {
        public static Guid GetCurrentUserId(this ClaimsPrincipal claims)
        {
            var userIdClaim = claims.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Guid.Empty;
            return Guid.Parse(userIdClaim.Value);
        }
        public static Guid GetSafeMasterWalletId(this ClaimsPrincipal claims)
        {
            var walletIdClaim = claims.FindFirst("safe-master-wallet-id");
            if (walletIdClaim == null)
                return Guid.Empty;
            return Guid.Parse(walletIdClaim.Value);
        }
    }
}