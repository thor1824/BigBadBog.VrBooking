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
            User createdUser;
            try
            {
                if (string.IsNullOrEmpty(user.Name))
                {
                    throw new InvalidDataException("user must contain a name");
                }
                if (!isEmailValid(user))
                {
                    throw new InvalidDataException("the email must be an easv365 mail");
                }

                if (!isPhoneNumberValid(user))
                {
                    throw new InvalidDataException("Phone Number must be 8 digits");
                }

                createdUser = _repo.Create(user);

                if (!isIdValid(createdUser))
                {
                    throw new InvalidOperationException("ID not valid");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
            return createdUser;
        }



        public User Read(long id)
        {
            User user;
            try
            {
                user = _repo.Read(id);
                if (user == null)
                {
                    throw new InvalidDataException("User does not exist");
                }
                if (user.Id != id)
                {
                    throw new InvalidOperationException("Error: UserService Read(id) retrieves wrong user");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
            return user;
        }

        public List<User> ReadAll()
        {
            try
            {
                return _repo.ReadAll().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public User Update(User user)
        {
            User updatedUser;
            try
            {
                if (!UserExist(user.Id))
                {
                    throw new InvalidDataException("User does not exist");
                }
                
                updatedUser = _repo.Update(user);

                if (updatedUser == null)
                {
                    throw new InvalidOperationException("Updated User was null");
                }
                
                if (user.Equals(Read(user.Id)))
                {
                    throw new InvalidOperationException("User was not Updated");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return updatedUser;
        }

        public User Delete(long id)
        {
            User deletedUser;
            try
            {
                deletedUser = _repo.Delete(Read(id));
                if (deletedUser == null)
                {
                    throw new InvalidOperationException("Deleted User was null");
                }
        
                if (UserExist(id))
                {
                    throw new InvalidOperationException("User was not deleted");
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return deletedUser;
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

        public bool isPhoneNumberValid(User user)
        {
            try
            {
                int.Parse(user.PhoneNumber);
            }
            catch (Exception)
            {
                return false;
            }
            if (string.IsNullOrEmpty(user.PhoneNumber) || user.PhoneNumber.Length != 8)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool isEmailValid(User user)
        {
            if (!user.SchoolMail.Contains("@easv365.dk"))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public bool isIdValid(User user)
        {
            if (user.Id <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
