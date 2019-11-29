namespace VrBooking.Core.Entity
{
    public class LoginUser
    {
        public UserInfo UserInfo { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActivated { get; set; }
    }
}
