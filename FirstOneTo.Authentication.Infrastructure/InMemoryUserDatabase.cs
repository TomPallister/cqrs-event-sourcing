using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.Security;

namespace FirstOneTo.Authentication.Infrastructure
{
    public class InMemoryUserDatabase : IReadModelUserDatabase
    {
        private readonly Dictionary<string, List<string>> _claims = new Dictionary<string, List<string>>();
        private readonly List<Tuple<string, string>> _users = new List<Tuple<string, string>>();

        public InMemoryUserDatabase()
        {
            _users.Add(new Tuple<string, string>("demo", "demo"));
            _claims.Add("demo", new List<string> {"demo", "admin"});

            _users.Add(new Tuple<string, string>("nonadmin", "nonadmin"));
            _claims.Add("nonadmin", new List<string> {"demo",});
        }

        public IUserIdentity GetUser(string userName, string password)
        {
            Tuple<string, string> user = _users.FirstOrDefault(x => x.Item1 == userName && x.Item2 == password);
            if (user == null)
            {
                return null;
            }

            List<string> claims = _claims[user.Item1];
            return new UserIdentity {UserName = user.Item1, Claims = claims};
        }

        public void AddUser(string userName, string password)
        {
            _users.Add(new Tuple<string, string>(userName, password));
            _claims.Add(userName, new List<string> {"demo",});
        }
    }
}