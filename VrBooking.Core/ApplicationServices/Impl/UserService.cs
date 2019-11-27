using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using VrBooking.Core.Entity;

namespace VrBooking.Core.ApplicationServices
{
    public class UserService : IUserService
    {
        private IRepository<User> _repo;
        public UserService(IRepository<User> repo)
        {
            this._repo = repo;
        }
        public User Create(User user)
        {
            if (string.IsNullOrEmpty(user.Name) )
            {
                throw new InvalidDataException("user must contain a name");
            }
            if (CheckSchoolMail(user))
            {
                throw new InvalidDataException("the email must be an easv365 mail");
            }

            if (CheckPhoneNumber(user))
            {
                throw new InvalidDataException("Phone Number must be 8 digits");
            }
            User user2 = _repo.Create(user);
            if (CheckUserId(user2))
            {
                throw new InvalidDataException("ID not valid");
            }

            return user2;
        }

        

        public User Read(long id)
        {
            User user = _repo.Read(id);
            if (user == null)
            {
                throw new InvalidOperationException("User does not exist");
            }

            return user;
        }
        public List<User> ReadAll()
        {
            return _repo.ReadAll().ToList();
        }
        public User Update(User user)
        {
            if (UserExist(user.Id))
            {
                throw new InvalidOperationException("User does not exist");
            }
            return _repo.Update(user);
        }
        public User Delete(long id)
        {
            if (UserExist(id))
            {
                throw new InvalidOperationException("User does not exist");
            }
            User user = _repo.Delete(Read(id));
            if (UserExist(user.Id))
            {
                throw new InvalidDataException("User was not deleted");
            }

            return user;

        }

        public bool UserExist(long id)
        {
            if (_repo.Read(id) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckPhoneNumber(User user)
        {
            int n;
            try
            {
                Int32.TryParse(user.PhoneNumber, out n);
            }
            catch (Exception e)
            {
                return true;
            }
            if (string.IsNullOrEmpty(user.PhoneNumber) || user.PhoneNumber.Length != 8)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckSchoolMail(User user)
        {
            if (!user.SchoolMail.Contains("@easv365.dk"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool CheckUserId(User user)
        {
            if (user.Id == null || user.Id <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
