namespace Infrastructure.Persistence.Configs
{
    class ConnectionStrings
    {
        private static string ipAddress = @"103.215.223.93"; // Local

        #region Identity Connection String
        private static string defaultCatalog = "techonit_wallet";
        private static string defaultUser = "techonit_walletusr";
        private static string defaultPassword = "xY21vj!5%fd5gwbo*R648";
        public static string Identity = $"Data Source={ipAddress};Initial Catalog={defaultCatalog};User ID={defaultUser};Password={defaultPassword};Persist Security Info=True;";
        #endregion        
    }
}