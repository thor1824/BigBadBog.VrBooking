using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using VrBooking.Core;
using VrBooking.Core.Entity;

namespace VrBooking.Infrastructure.Repositories
{
    public class UserRepository: IRepository<User>
    {
        private readonly VrBookingContext _contex;
        public UserRepository(VrBookingContext contex)
        {
            _contex = contex;
        }
        public User Create(User entity)
        {
            User userToCreate = _contex.users.Add(entity).Entity;
            _contex.SaveChanges();
            return userToCreate;

        }

        public User Read(long id)
        {
            return _contex.users.FirstOrDefault(user => user.Id == id);
        }

        public IEnumerable<User> ReadAll()
        {
            return _contex.users;
        }

        public User Update(User entity)
        {
            User oldeUser = Read(entity.Id);

            oldeUser.Address = entity.Address;
            oldeUser.Name = entity.Name;
            oldeUser.PhoneNumber = entity.PhoneNumber;
            oldeUser.SchoolMail = entity.SchoolMail;

            /*
            if (entity != null)
            {
                _contex.Attach(entity).State = EntityState.Modified;
            }

            */
            _contex.SaveChanges();
            return entity;
        }

        public User Delete(User entity)
        {
            var entityRemoved = _contex.Remove(Read(entity.Id)).Entity;
            _contex.SaveChanges();
            return entityRemoved;
        }
    }
}
