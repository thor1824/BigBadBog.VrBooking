namespace VrBooking.Core.Entity
{
    public class LoginUser
    {
        public User User { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool Admin { get; set; }
        public bool Activated { get; set; }
    }
}
