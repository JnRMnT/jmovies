using JMovies.DataAccess;
using JMovies.Entities;
using JMovies.Entities.Interfaces;
using JMovies.Entities.Requests;
using JMovies.Entities.Responses;
using JMovies.Entities.UserManagement;
using JMovies.Utilities.Hashing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace JMovies.App.Business.Actions
{
    public class Register : IActionClass
    {
        public void ExecuteAction(IServiceProvider serviceProvider, ref object request, ref BaseResponse response)
        {
            RegisterRequest requestMessage = request as RegisterRequest;
            RegisterResponse responseMessage = response as RegisterResponse;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            }))
            {
                using (JMoviesEntities entities = new JMoviesEntities())
                {
                    string salt = HashHelper.GenerateSalt();
                    string hash = HashHelper.Hash(HashTypeEnum.Sha512, requestMessage.Password, salt);
                    User user = new User
                    {
                        Email = requestMessage.Email,
                        Password = new Password
                        {
                            Hash = hash,
                            HashType = HashTypeEnum.Sha512,
                            ModifyDate = DateTime.Now,
                            RetryCount = 0,
                            Salt = salt
                        },
                        RegistrationDate = DateTime.Now,
                        UserName = requestMessage.Username.ToPlainString()
                    };
                    entities.Password.Add(user.Password);
                    entities.User.Add(user);
                    entities.SaveChanges();
                    scope.Complete();
                }
            }
        }
    }
}
