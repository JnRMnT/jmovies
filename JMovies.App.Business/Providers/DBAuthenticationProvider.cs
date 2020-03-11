using JMovies.Common.Constants;
using JMovies.DataAccess;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;
using JMovies.Entities.UserManagement;
using JMovies.Utilities.Hashing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace JMovies.App.Business.Providers
{
    public class DBAuthenticationProvider : IAuthenticationProvider
    {
        public LoginResponse Authenticate(LoginRequest request)
        {
            LoginResponse response = new LoginResponse();
            using (JMoviesEntities entities = new JMoviesEntities())
            {
                response.Result = Entities.UserManagement.LoginResultEnum.Undefined;
                if (request.Username != null && request.Password != null)
                {
                    response.Result = LoginResultEnum.WrongUsernameOrPassword;
                    User user = entities.User.Include(e=> e.Password).SingleOrDefault(e => e.UserName == request.Username.ToPlainString());
                    if (user != null)
                    {
                        if (user.Password != null)
                        {
                            if (HashHelper.Matches(request.Password, user.Password.Hash, user.Password.Salt, user.Password.HashType))
                            {
                                if (user.Password.RetryCount >= ConfigurationConstants.MaxPasswordRetryCount)
                                {
                                    //User blocked
                                    response.Result = LoginResultEnum.UserBlocked;
                                }
                                else
                                {
                                    user.Password.ModifyDate = DateTime.Now;
                                    response.Result = LoginResultEnum.Successful;
                                    response.User = user;
                                }
                            }
                            else
                            {
                                //Wrong Password
                                user.Password.ModifyDate = DateTime.Now;
                                user.Password.RetryCount++;
                                if (user.Password.RetryCount >= ConfigurationConstants.MaxPasswordRetryCount)
                                {
                                    user.Password.RetryCount = ConfigurationConstants.MaxPasswordRetryCount;
                                }
                            }
                            entities.SaveChanges();
                        }
                    }
                }
            }

            return response;
        }
    }
}
