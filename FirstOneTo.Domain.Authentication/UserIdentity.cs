using System.Collections.Generic;

namespace FirstOneTo.Authentication
{
    public class UserIdentity : IUser
    {
        public UserIdentity()
        {
        }

        public UserIdentity(long aggregateId,
            string userName,
            string password,
            string email,
            IEnumerable<string> claims)
        {
            AggregateId = aggregateId;
            Password = password;
            UserName = userName;
            Claims = claims;
            Email = email;
        }

        public string Password { get; set; }
        public long AggregateId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; }
    }
}