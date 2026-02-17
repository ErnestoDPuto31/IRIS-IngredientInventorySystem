namespace IRIS.Domain.Entities
{
    public static class UserSession
    {
        public static User? CurrentUser { get; set; }
        public static string? CurrentSessionToken { get; set; }
    }
}
