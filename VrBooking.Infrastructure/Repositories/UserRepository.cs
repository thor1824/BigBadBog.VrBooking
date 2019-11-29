using System;
using System.Collections.Generic;
using System.Linq;
using VrBooking.Core.DomainServices;
using VrBooking.Core.Entity;

namespace VrBooking.Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly VrBookingContext _contex;
        public UserRepository(VrBookingContext contex)
        {
            _contex = contex;
        }
        public User Create(User entity)
        {
            _contex.Add<User>(entity);
            _contex.SaveChanges();
            throw new NotImplementedException();
        }

        public User Read(long id)
        {
            return _contex.Users.FirstOrDefault(user => user.Id == id);
        }

        public IEnumerable<User> ReadAll()
        {
            return _contex.Users;
        }

        public User Update(User entity)
        {
            User user = _contex.Update<User>(entity).Entity;
            _contex.SaveChanges();
            return user;
        }

        public User Delete(User entity)
        {
            User entityRemoved = _contex.Remove<User>(entity).Entity;
            _contex.SaveChanges();
            return entityRemoved;
        }
    }
}
