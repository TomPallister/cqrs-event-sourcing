using System;
using System.Threading.Tasks;
using FirstOneTo.Readmodel.Models;
using Nancy;
using Nancy.Security;

namespace FirstOneTo.Authentication.Service
{
    public interface IAuthenticationService
    {
        IUser GetUser(string userName, string password);
        IUser GetUserByEmail(string email, string password);
        string Tokenize(IUser userIdentity, NancyContext nancyContext);
        IUserIdentity Detokenize(NancyContext nancyContext);
        void AddUser(string userName, string password, long aggregateId, string email);
        void ResetPassword(long aggregateId, string password);
        void AddUser(string userName, string password, long aggregateId, string email, string facebookId);
        IUser GetUser(string userName);
        IUser GetUserByEmail(string email);
        IUser GetUser(long aggregateId);
    }
}