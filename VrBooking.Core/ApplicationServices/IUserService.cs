using System.Collections.Generic;
using VrBooking.Core.Entity;

namespace VrBooking.Core.ApplicationServices
{
    interface IUserService
    {
        User Create(User user);
        User Delete(long id);
        User Read(long id);
        List<User> ReadAll();
        User Update(User user);
    }
}