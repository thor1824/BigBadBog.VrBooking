using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using VrBooking.Core.DomainServices;
using VrBooking.Core.Entity;

namespace VrBooking.Core.ApplicationServices
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repo;
        public UserService(IRepository<User> repo)
        {
            _repo = repo;
        }

        #region C.R.U.D
        public User Create(User user)
        {
            User createdUser;
            try
            {
                if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
                {
                    throw new InvalidDataException("user must contain a name");
                }
                if (!IsEmailValid(user))
                {
                    throw new InvalidDataException("the email must be an easv365 mail");
                }
                if (!IsPhoneNumberValid(user))
                {
                    throw new InvalidDataException("Phone Number must be 8 digits");
                }

                createdUser = _repo.Create(user);

                if (!IsIdValid(createdUser))
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
                if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
                {
                    throw new InvalidDataException("user must contain a name");
                }
                if (!UserExist(user.Id))
                {
                    throw new InvalidDataException("User does not exist");
                }
                if (!IsEmailValid(user))
                {
                    throw new InvalidDataException("the email must be an easv365 mail");
                }

                if (!IsPhoneNumberValid(user))
                {
                    throw new InvalidDataException("Phone Number must be 8 digits");
                }

                updatedUser = _repo.Update(user);

                if (updatedUser == null)
                {
                    throw new InvalidOperationException("Updated User was null");
                }

                if (!user.Equals(Read(user.Id)))
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
        #endregion

        #region validation
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

        public bool IsPhoneNumberValid(User user)
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

        public bool IsEmailValid(User user)
        {
            Regex regexItem = new Regex("^([\\w\\.\\-_]+)@easv365.dk*$");
            if (!string.IsNullOrEmpty(user.SchoolMail))
            {
                return regexItem.IsMatch(user.SchoolMail);
            }
            return false;

        }
        public bool IsIdValid(User user)
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
        #endregion
    }
}
