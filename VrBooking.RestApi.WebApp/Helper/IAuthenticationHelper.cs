using VrBooking.Core.Entity;

namespace VrBooking.RestApi.WebApp.Helper
{
    public interface IAuthenticationHelper
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        string GenerateToken(LoginUser user);
        
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}
