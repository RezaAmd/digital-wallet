namespace System.Security.Claims
{
    public static class CustomPrincipal
    {
        public static string GetCurrentWalletId(this ClaimsPrincipal claims)
        {
            var walletIdClaim = claims.FindFirst("wallet-id");
            if(walletIdClaim == null)
                return string.Empty;
            return walletIdClaim.Value;
        }
    }
}