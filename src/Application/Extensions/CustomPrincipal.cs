namespace System.Security.Claims
{
    public static class CustomPrincipal
    {
        public static string GetCurrentWalletId(this ClaimsPrincipal claims)
        {
            return claims.FindFirst("wallet-id").Value;
        }
    }
}