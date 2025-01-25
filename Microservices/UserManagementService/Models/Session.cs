namespace UserManagementService.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}