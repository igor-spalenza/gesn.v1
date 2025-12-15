namespace gesn.webApp.Areas.Admin.Models.Home
{
    public class UserStatsViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int RoleCount { get; set; }
        public int ClaimCount { get; set; }
        public bool IsActive { get; set; }
    }

}
