namespace gesn.webApp.Areas.Admin.Models.Home
{
    public class ClaimStatsViewModel
    {
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public int UserCount { get; set; }
        public int RoleCount { get; set; }
        public int TotalAssignments => UserCount + RoleCount;
    }

}
