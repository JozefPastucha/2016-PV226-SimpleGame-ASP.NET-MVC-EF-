namespace BL.Utils.AccountPolicy
{
    /// <summary>
    /// Defines various roles used within authentication
    /// </summary>
    public static class Claims
    {
        public const string Player = "Player";

        public const string Admin = "Administrator";

        public const string AuthenticatedUsers = "Player, Administrator";
    }
}
