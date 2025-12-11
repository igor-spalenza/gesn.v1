namespace gesn.webApp.Models
{
    public class ApplicationRoleClaim<T> where T : IEquatable<T>
    {
        public int Id { get; set; }

        public T RoleId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}