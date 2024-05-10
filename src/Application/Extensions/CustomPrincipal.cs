using System.Security.Claims;

namespace DigitalWallet.Application.Extensions
{
    public static class CustomPrincipal
    {
        public static Guid GetCurrentWalletId(this ClaimsPrincipal claims)
        {
            var walletIdClaim = claims.FindFirst("wallet-id");
            if (walletIdClaim == null)
                return Guid.Empty;
            return Guid.Parse(walletIdClaim.Value);
        }
    }
}