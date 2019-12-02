using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using VrBooking.Core.Entity;

namespace VrBooking.Core.ApplicationServices
{
    public class LoginUserService : ILoginUserService
    {
        private readonly IRepository<LoginUser> _repo;

        public LoginUserService(IRepository<LoginUser> repo)
        {
            this._repo = repo;
        }


        #region C.R.U.D




        public LoginUser Create(LoginUser loginUser)
        {
            LoginUser createdLoginUser;
            try
            {
                if (IsUserNameNull(loginUser))
                {
                    throw new InvalidDataException("user must contain a name");
                }

                if (IsUserNameValid(loginUser))
                {
                    throw new InvalidDataException("username contains special caterers");
                }

                if (IsUsernameInUse(loginUser))
                {
                    throw new InvalidDataException(" this email is in use");
                }
                if (DoesContainPassword(loginUser))
                {
                    throw new InvalidDataException("LoginUser dos not contain password");
                }
                if (DoesContainSalt(loginUser))
                {
                    throw new InvalidDataException("LoginUser dos not contain passwordSalt");
                }

                createdLoginUser = _repo.Create(loginUser);

                if (IsIdValid(createdLoginUser))
                {
                    throw new InvalidOperationException("Id not valid");
                }
            }
            catch (Exception e)
            {
                throw e;
            }


            return createdLoginUser;
        }

        public LoginUser Read(long id)
        {
            LoginUser user;
            try
            {
                user = _repo.Read(id);

                if (user == null)
                {
                    throw new InvalidDataException("User does not exist");
                }
                if (user.Id != id)
                {
                    throw new InvalidOperationException("Error: LoginUserService Read(id) retrieves wrong loginUser");
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return user;
        }

        public List<LoginUser> ReadAll()
        {
            List<LoginUser> loginUsers;
            try
            {
                loginUsers = _repo.ReadAll().ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            return loginUsers;
        }
        public LoginUser Update(LoginUser loginUser)
        {
            LoginUser updateUser;
            try
            {
                if (LoginUserExist(loginUser.Id))
                {
                    throw new InvalidDataException("LoginUser does not exist");
                }

                if (DoesContainPassword(loginUser))
                {
                    throw new InvalidDataException("LoginUser must have a password");
                }

                if (IsUserNameValid(loginUser))
                {
                    throw new InvalidDataException("The new username is not valid. It must be a @easv365.dk mail and cant contain special caterers");
                }
                if (DoesContainPassword(loginUser))
                {
                    throw new InvalidDataException("LoginUser dos not contain password");
                }
                if (DoesContainSalt(loginUser))
                {
                    throw new InvalidDataException("LoginUser dos not contain passwordSalt");
                }

                updateUser = _repo.Update(loginUser);

                if (updateUser == null)
                {
                    throw new InvalidOperationException("Updated User was null");
                }

                if (IsUserUpdated(loginUser))
                {
                    throw new InvalidOperationException("User was not Updated");
                }

            }

            catch (Exception e)
            {
                throw e;
            }

            return updateUser;

        }
        public LoginUser Delete(long id)
        {
            LoginUser deletedUser;
            try
            {
                deletedUser = _repo.Delete(Read(id));

                if (deletedUser == null)
                {
                    throw new InvalidOperationException("User was not found");
                }

                if (!LoginUserExist(id))
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

        public bool LoginUserExist(long id)
        {
            if (_repo.Read(id) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsIdValid(LoginUser loginUser)
        {
            if (loginUser.Id <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }



        }

        public bool IsUserNameNull(LoginUser user)
        {
            if (string.IsNullOrEmpty(user.UserName))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool IsUserNameValid(LoginUser user)
        {

            var regexItem = new Regex("^([\\w\\.\\-_]+)@easv365.dk*$");


            if (regexItem.IsMatch(user.UserName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsUsernameInUse(LoginUser user)
        {
            LoginUser loginUser =
                _repo.ReadAll().Where(x => x.UserName.ToLower().Equals(user.UserName.ToLower())).FirstOrDefault();
            Console.WriteLine(loginUser);
            return loginUser != null;
        }



        public bool DoesContainPassword(LoginUser user)
        {
            if (user.PasswordHash == null || user.PasswordHash.Length <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DoesContainSalt(LoginUser user)
        {
            if (user.PasswordSalt == null || user.PasswordSalt.Length <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsUserUpdated(LoginUser user)
        {
            LoginUser userToUpdate = Read(user.Id);


            if (user.Activated != userToUpdate.Activated
                || user.Admin != userToUpdate.Admin
                || user.Id != userToUpdate.Id
                || user.UserName != userToUpdate.UserName
                || user.PasswordHash != userToUpdate.PasswordHash
                || user.PasswordSalt != userToUpdate.PasswordSalt)
            {
                return false;
            }

            return true;
        }



        #endregion

    }

}