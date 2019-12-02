using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
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


        #region C.R.U.D



        
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

                if (isUsernameInUse(loginUser))
                {
                    throw new InvalidDataException(" this email is in use");
                }

                
                createdLoginUser = _repo.Create(loginUser);

                Console.WriteLine("1");

                if (isIdValid(createdLoginUser))
                {
                    throw new InvalidOperationException("Id not valid");
                }

                Console.WriteLine("2");
                if (dosContainPassword(createdLoginUser))
                {
                    throw new InvalidOperationException("LoginUser dos not contain password");
                }

                Console.WriteLine("3");
                if (dosContainSalt(createdLoginUser))
                {
                    throw new InvalidOperationException("LoginUser dos not contain passwordSalt");
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
            
            return _repo.ReadAll().ToList();
            
            
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

                if (dosContainPassword(loginUser))
                {
                    throw new InvalidDataException("LoginUser must have a password");
                }

                if (isUserNameValid(loginUser))
                {
                    throw new InvalidDataException("The new username is not valid. It must be a @easv365.dk mail and cant contain special caterers");
                }
                
                updateUser = _repo.Update(loginUser);

                if (updateUser == null)
                {
                    throw new InvalidOperationException("Updated User was null");
                }

                if (loginUser.Equals(Read(loginUser.Id)))
                {
                    throw new InvalidOperationException("User was not Updated");
                }

            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
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

        public bool isIdValid(LoginUser loginUser)
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

        public bool isUserNameNull(LoginUser user)
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

        public bool isUsernameInUse(LoginUser user)
        {
            foreach (LoginUser x in ReadAll())
            {
                if (x.UserName == user.UserName)
                {
                    return true;
                }
               
            }
            
            
            return false;
            

        }

    

        public bool dosContainPassword(LoginUser user)
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

        public bool dosContainSalt(LoginUser user)
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



        #endregion
       
    }

}
//whitspaces tegn array toLower mellem 3 og 20 tegn 
   // salt og password