using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VrBooking.Core.DomainServices;
using VrBooking.Core.Entity;

namespace VrBooking.Infrastructure.Repositories
{
    public class LoginUserRepository : IRepository<LoginUser>
    {
        private readonly VrBookingContext _ctx;


        public LoginUserRepository(VrBookingContext ctx)
        {
            _ctx = ctx;
        }


        public LoginUser Create(LoginUser entity)
        {
            LoginUser createdLoginUser = _ctx.LoginUsers.Add(entity).Entity;
            _ctx.SaveChanges();
            return createdLoginUser;
        }

        public LoginUser Read(long id)
        {
            return _ctx.LoginUsers.FirstOrDefault(user => user.Id == id);
        }

        public IEnumerable<LoginUser> ReadAll()
        {
            return _ctx.LoginUsers.Include(lu => lu.UserInfo);
        }

        public LoginUser Update(LoginUser entity)
        {
            LoginUser oldUser = Read(entity.Id);

            oldUser.IsActivated = entity.IsActivated;
            oldUser.IsAdmin = entity.IsAdmin;
            oldUser.UserInfo = entity.UserInfo;
            oldUser.PasswordHash = entity.PasswordHash;
            oldUser.PasswordSalt = entity.PasswordSalt;

            _ctx.SaveChanges();

            return entity;
        }

        public LoginUser Delete(LoginUser entity)
        {
            LoginUser userToDelete = _ctx.Remove(Read(entity.Id)).Entity;
            _ctx.SaveChanges();
            return userToDelete;
        }
    }
}
