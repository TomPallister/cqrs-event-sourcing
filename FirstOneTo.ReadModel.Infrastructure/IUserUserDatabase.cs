using System.Collections.Generic;
using System.Threading.Tasks;
using FirstOneTo.Authentication;

namespace FirstOneTo.ReadModel.Infrastructure
{
    public interface IReadModelUserDatabase
    {
        Task ResetPasswordAsync(long aggregateId, string password);
        Task<UserIdentity> GetUserAsync(string userName, string password);
        Task AddUserAsync(string userName, string password, long aggregateId, List<string> claims, string email);
        Task AddUserAsync(string userName, string password, long aggregateId, List<string> claims, string email,
            string facebookId);
        Task<UserIdentity> GetUserAsync(string userName);
        Task<UserIdentity> GetUserAsync(long aggregateId);
        Task<UserIdentity> GetUserByEmailAsync(string email);
        //sync
        void ResetPassword(long aggregateId, string password);
        UserIdentity GetUser(string userName, string password);
        void AddUser(string userName, string password, long aggregateId, List<string> claims, string email);
        void AddUser(string userName, string password, long aggregateId, List<string> claims, string email,
            string facebookId);
        UserIdentity GetUser(string userName);
        UserIdentity GetUser(long aggregateId);
        UserIdentity GetUserByEmail(string email);
    }
}