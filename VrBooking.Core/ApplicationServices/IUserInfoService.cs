using System.Collections.Generic;
using VrBooking.Core.Entity;

namespace VrBooking.Core.ApplicationServices
{
    public interface IUserInfoService
    {
        UserInfo Create(UserInfo user);
        UserInfo Delete(long id);
        UserInfo Read(long id);
        List<UserInfo> ReadAll();
        UserInfo Update(UserInfo user);
    }
}