using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using VrBooking.Core.Entity;

namespace VrBooking.Core.ApplicationServices
{
    public class LoginUserService : ILoginUserService
    {
        private IRepository<LoginUser> _repo;

        public LoginUserService(IRepository<LoginUser> repo)
        {
            this._repo = repo;
        }
        public LoginUser Create(LoginUser loginUser)
        {
            LoginUser createdLoginUser;
            try
            {
                if (isUserNameNull(loginUser))
                {
                    throw new InvalidDataException("user must contain a name");
                }

                if (isUserNameValid(loginUser))
                {
                    throw new InvalidDataException("username contains special caterers");
                }

                
                createdLoginUser = _repo.Create(loginUser);

                if (isIdValid(createdLoginUser))
                {
                    throw new InvalidDataException("Id not valid");
                }

                if (dosContainPassword(createdLoginUser))
                {
                    throw new InvalidDataException("LoginUser dos not contain password");
                }

                if (dosContainSalt(createdLoginUser))
                {
                    throw new InvalidDataException("LoginUser dos not contain passwordSalt");
                }



            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return createdLoginUser;
        }

        public LoginUser Read(long id)
        {
            LoginUser user;
            try
            {
                user = _repo.Read(id);
                {
                    if (user == null)
                    {
                        throw new InvalidDataException("User does not exist");
                    }
                    if (user.Id != id)
                    {
                        throw new InvalidOperationException("Error: LoginUserService Read(id) retrieves wrong loginUser");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return user;
        }

        public List<LoginUser> ReadAll()
        {
            return null;
        }
        public LoginUser Update(LoginUser loginUser)
        {
            return null;
        }
        public LoginUser Delete(long id)
        {
            return null;
        }


        #region validation 


        public bool LoginUserExist(long id)
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

        public bool isIdValid(LoginUser loginUser)
        {
            if (loginUser.Id <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }

            

        }

        public bool isUserNameNull(LoginUser user)
        {
            if (string.IsNullOrEmpty(user.UserName))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool isUserNameValid(LoginUser user)
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


    

        public bool dosContainPassword(LoginUser user)
        {
            if (user.PasswordHash == null && user.PasswordHash.Length < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool dosContainSalt(LoginUser user)
        {
            if (user.PasswordSalt == null && user.PasswordSalt.Length < 0)
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
//whitspaces tegn array toLower mellem 3 og 20 tegn 
   // salt og password