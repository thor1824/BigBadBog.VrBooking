using System;
using System.Collections.Generic;
using System.Linq;
using VrBooking.Core.DomainServices;
using VrBooking.Core.Entity;

namespace VrBooking.Infrastructure.Repositories
{
    public class UserInfoRepository : IRepository<UserInfo>
    {
        private readonly VrBookingContext _contex;
        public UserInfoRepository(VrBookingContext contex)
        {
            _contex = contex;
        }
        public UserInfo Create(UserInfo entity)
        {
            _contex.Add<UserInfo>(entity);
            _contex.SaveChanges();
            throw new NotImplementedException();
        }

        public UserInfo Read(long id)
        {
            return _contex.Users.FirstOrDefault(user => user.Id == id);
        }

        public IEnumerable<UserInfo> ReadAll()
        {
            return _contex.Users;
        }

        public UserInfo Update(UserInfo entity)
        {
            UserInfo oldUserInfo = _contex.Users.First(userInfo => userInfo.Id == entity.Id);
            oldUserInfo.FirstName = entity.FirstName;
            oldUserInfo.LastName = entity.LastName;
            oldUserInfo.Email = entity.Email;
            oldUserInfo.PhoneNumber = entity.PhoneNumber;
            oldUserInfo.Address = entity.Address;
            _contex.SaveChanges();
            return oldUserInfo;
        }

        public UserInfo Delete(UserInfo entity)
        {
            UserInfo entityRemoved = _contex.Remove<UserInfo>(entity).Entity;
            _contex.SaveChanges();
            return entityRemoved;
        }
    }
}
