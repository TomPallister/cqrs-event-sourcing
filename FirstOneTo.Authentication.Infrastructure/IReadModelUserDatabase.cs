using Nancy.Security;

namespace FirstOneTo.Authentication.Infrastructure
{
    public interface IReadModelUserDatabase
    {
        IUserIdentity GetUser(string userName, string password);
        void AddUser(string userName, string password);
    }
}