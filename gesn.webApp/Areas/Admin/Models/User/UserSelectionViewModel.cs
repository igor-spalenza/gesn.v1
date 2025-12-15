namespace gesn.webApp.Areas.Admin.Models.User
{
    public class UserSelectionViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
    }
}
