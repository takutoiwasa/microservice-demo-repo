namespace UserManagementService.Models
{
    public class User
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // パスワードフィールドの追加
    }
}