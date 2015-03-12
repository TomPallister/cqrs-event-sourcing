using System.Linq;
using System.Threading.Tasks;
using FirstOneTo.ReadModel.Infrastructure;
using FirstOneTo.ReadModel.Infrastructure.InMemory;
using FluentAssertions;
using Nancy;
using Nancy.Authentication.Token;
using NUnit.Framework;

namespace FirstOneTo.Authentication.Service.Tests
{
    [TestFixture]
    public class BCryptTests
    {
        [SetUp]
        public void SetUp()
        {
            _userDatabase = new InMemoryUserDatabase();
            _authenticationService = new AuthenticationService(_userDatabase);
        }

        private IReadModelUserDatabase _userDatabase;
        private IAuthenticationService _authenticationService;

        [Test]
        public async Task can_hash_password()
        {
            string password = "LittleBalls";
            password = BCrypt.Net.BCrypt.HashPassword(password);
            password.Should().NotBe("LittleBalls");
        }

        [Test]
        public async Task can_unhash_password()
        {
            const string password = "LittleBalls";
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            BCrypt.Net.BCrypt.Verify(password, hash).Should().BeTrue();
        }


    }
}