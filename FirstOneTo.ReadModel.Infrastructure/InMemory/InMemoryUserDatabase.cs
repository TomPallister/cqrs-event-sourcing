using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstOneTo.Authentication;

namespace FirstOneTo.ReadModel.Infrastructure.InMemory
{
    public class InMemoryUserDatabase : IReadModelUserDatabase
    {
        private readonly List<UserIdentity> _userIdentities = new List<UserIdentity>();

        public async Task ResetPasswordAsync(long aggregateId, string password)
        {
             await Task.Run(() => UpdateUser(aggregateId, password));
        }

        public void ResetPassword(long aggregateId, string password)
        {
            UpdateUser(aggregateId, password);
        }

        private void UpdateUser(long aggregateId, string password)
        {
            UserIdentity user = _userIdentities.FirstOrDefault(x => x.AggregateId == aggregateId);
            if (user != null) user.Password = password;
        }


        public async Task AddUserAsync(string userName, string password, long aggregateId, List<string> claims, string email)
        {
             await Task.Run(() => AddUser(userName, password, aggregateId, claims, email));
        }

        public void AddUser(string userName, string password, long aggregateId, List<string> claims, string email)
        {
            if (userName != "nonadmin")
            {
                _userIdentities.Add(new UserIdentity
                {
                    UserName = userName,
                    Password = password,
                    Claims = claims,
                    AggregateId = aggregateId,
                    Email = email
                });
            }
            else
            {
                _userIdentities.Add(new UserIdentity
                {
                    UserName = userName,
                    Password = password,
                    Claims = claims,
                    AggregateId = aggregateId,
                    Email = email
                });
            }
        }

        public async Task AddUserAsync(string userName, string password, long aggregateId, List<string> claims, string email, string facebookId)
        {
            await Task.Run(() => AddUser(userName, password, aggregateId, claims, email, facebookId));
        }

        public void AddUser(string userName, string password, long aggregateId, List<string> claims, string email,
            string facebookId)
        {
            if (userName != "nonadmin")
            {
                _userIdentities.Add(new UserIdentity
                {
                    UserName = userName,
                    Password = password,
                    Claims = claims,
                    AggregateId = aggregateId,
                    Email = email
                });
            }
            else
            {
                _userIdentities.Add(new UserIdentity
                {
                    UserName = userName,
                    Password = password,
                    Claims = claims,
                    AggregateId = aggregateId,
                    Email = email
                });
            }
        }

        public async Task<UserIdentity> GetUserAsync(string userName, string password)
        {
            return await Task.Run(() => GetUser(userName));
        }

        public async Task<UserIdentity> GetUserAsync(string userName)
        {
            return await Task.Run(() => GetUser(userName));
        }

        public async Task<UserIdentity> GetUserByEmailAsync(string email)
        {
            return await Task.Run(() => GetUserByEmail(email));
        }

        public async Task<UserIdentity> GetUserAsync(long aggregateId)
        {
            return await Task.Run(() => GetUser(aggregateId));
        }


        public UserIdentity GetUser(string userName, string password)
        {
            UserIdentity user = _userIdentities.FirstOrDefault(x => x.UserName == userName && x.Password == password);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public UserIdentity GetUser(string userName)
        {
            UserIdentity user = _userIdentities.FirstOrDefault(x => x.UserName == userName);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public UserIdentity GetUserByEmail(string email)
        {
            UserIdentity user = _userIdentities.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public UserIdentity GetUser(long aggregateId)
        {
            UserIdentity user = _userIdentities.FirstOrDefault(x => x.AggregateId == aggregateId);
            return user;
        }
    }
}