namespace VrBooking.Core.Entity
{
    public class LoginUser
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool Admin { get; set; }
        public bool Activated { get; set; }
    }
}
