using System.Collections.Generic;
using VrBooking.Core.Entity;

namespace VrBooking.Core.ApplicationServices
{
    public interface ILoginUserService
    {
        LoginUser Create(LoginUser loginUser);
        LoginUser Delete(long id);
        LoginUser Read(long id);
        List<LoginUser> ReadAll();
        LoginUser Update(LoginUser loginUser);
        LoginUser Login(string UserName);
    }
}