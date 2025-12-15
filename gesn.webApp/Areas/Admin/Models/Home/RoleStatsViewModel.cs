namespace gesn.webApp.Areas.Admin.Models.Home
{
    public class RoleStatsViewModel
    {
        public string RoleId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public int UserCount { get; set; }
        public int ClaimCount { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
