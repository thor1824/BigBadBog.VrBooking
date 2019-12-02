using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VrBooking.Core;
using VrBooking.Core.Entity;

namespace VrBooking.Infrastructure.Repositories
{
    class LoginUserRepository: IRepository<LoginUser>
    {
        private VrBookingContext _context;


        public LoginUserRepository(VrBookingContext contex)
        {
            contex = _context;
        }


        public LoginUser Create(LoginUser entity)
        {
            LoginUser createdLoginUser = _context.loginUsers.Add(entity).Entity;
            _context.SaveChanges();
            return createdLoginUser;
        }

        public LoginUser Read(long id)
        {
            return _context.loginUsers.FirstOrDefault(user => user.Id == id);
        }

        public IEnumerable<LoginUser> ReadAll()
        {
            return _context.loginUsers;
        }

        public LoginUser Update(LoginUser entity)
        {
            LoginUser oldUser = Read(entity.Id);

            oldUser.Activated = entity.Activated;
            oldUser.Admin = entity.Admin;
            oldUser.UserName = entity.UserName;
            oldUser.PasswordHash = entity.PasswordHash;
            oldUser.PasswordSalt = entity.PasswordSalt;

            _context.SaveChanges();

            return entity;
        }

        public LoginUser Delete(LoginUser entity)
        {
            LoginUser userToDelete = _context.Remove(Read(entity.Id)).Entity;
            _context.SaveChanges();
            return userToDelete;
        }
    }
}
