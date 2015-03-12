using System;
using System.Collections.Generic;
using FirstOneTo.Helpers;
using FirstOneTo.ReadModel.Infrastructure;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Security;

namespace FirstOneTo.Authentication.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IReadModelUserDatabase _userDatabase;

        public AuthenticationService(IReadModelUserDatabase userDatabase)
        {
            _userDatabase = userDatabase;
        }

        public void AddUser(string userName, string password, long aggregateId, string email)
        {
            //todo get rid of this logic it is because the claims havent been implemented properly
            _userDatabase.AddUser(userName, password, aggregateId, new List<string> {userName, userName},
                email);
        }

        public void AddUser(string userName, string password, long aggregateId, string email, string facebookId)
        {
            //todo get rid of this logic it is because the claims havent been implemented properly
            _userDatabase.AddUser(userName, password, aggregateId, new List<string> {userName, userName},
                email, facebookId);
        }

        public void ResetPassword(long aggregateId, string password)
        {
            _userDatabase.ResetPassword(aggregateId, password);
        }


        public IUser GetUser(string userName, string password)
        {
            UserIdentity user = _userDatabase.GetUser(userName);
            if (user != null && BCryptHelpers.DoesPasswordMatch(user.Password, password))
            {
                return user;
            }
            return null;
        }

        public IUser GetUserByEmail(string email, string password)
        {
            UserIdentity user = _userDatabase.GetUserByEmail(email);
            if (user != null && BCryptHelpers.DoesPasswordMatch(user.Password, password))
            {
                return user;
            }
            return null;
        }

        public IUser GetUser(string userName)
        {
            return _userDatabase.GetUser(userName);
        }

        public IUser GetUserByEmail(string email)
        {
            return _userDatabase.GetUserByEmail(email);
        }

        public IUser GetUser(long aggregateId)
        {
            return _userDatabase.GetUser(aggregateId);
        }


        public string Tokenize(IUser userIdentity, NancyContext nancyContext)
        {
            throw new NotImplementedException();
        }

        public IUserIdentity Detokenize(NancyContext nancyContext)
        {
            throw new NotImplementedException();
        }

    }
}