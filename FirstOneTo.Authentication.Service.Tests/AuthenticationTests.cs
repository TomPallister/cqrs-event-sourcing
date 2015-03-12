using System;
using System.Linq;
using System.Threading.Tasks;
using FirstOneTo.Helpers;
using FirstOneTo.ReadModel.Infrastructure;
using FirstOneTo.ReadModel.Infrastructure.InMemory;
using FluentAssertions;
using Nancy;
using Nancy.Authentication.Token;
using NUnit.Framework;

namespace FirstOneTo.Authentication.Service.Tests
{
    [TestFixture]
    public class AuthenticationTests
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
        public void can_add_facebook_user()
        {
            string userName = "Tom";
            string password = "LittleBalls";
            long id = 1;
            string email = "someemail";
            string facebookId = "somefacebookId";
             _authenticationService.AddUser(userName, BCryptHelpers.SetPassword(password), id, email, facebookId);

            IUser user =  _authenticationService.GetUser(userName, password);
            user.UserName.Should().Be(userName);
        }

        [Test]
        public void can_add_user()
        {
            string userName = "Tom";
            string password = "LittleBalls";
            long id = 1;
             _authenticationService.AddUser(userName, BCryptHelpers.SetPassword(password), id, "someemail");

            IUser user =  _authenticationService.GetUser(userName, password);
            user.UserName.Should().Be(userName);
        }



        [Test]
        public void can_get_user_by_email()
        {
            string userName = "Tom";
            string password = "LittleBalls";
            long id = 1;
            string email = "thomasgardhamthebomb@gmail.com";
             _authenticationService.AddUser(userName, password, id, email);
            IUser user =  _authenticationService.GetUserByEmail(email);
            user.UserName.Should().Be(userName);
        }

        [Test]
        public void can_get_user_by_username()
        {
            string userName = "Tom";
            string password = "LittleBalls";
            long id = 1;
            string email = "thomasgardhamthebomb@gmail.com";
             _authenticationService.AddUser(userName, password, id, email);
            IUser user =  _authenticationService.GetUser(userName);
            user.UserName.Should().Be(userName);
        }

        [Test]
        public void can_get_user_by_username_and_password()
        {
            string userName = "demo";
            string password = "demo";
            long id = 1;
             _authenticationService.AddUser(userName, BCryptHelpers.SetPassword(password), id, "someemail");
            IUser user =  _authenticationService.GetUser(password, password);
            user.UserName.Should().Be(userName);
        }
    }
}