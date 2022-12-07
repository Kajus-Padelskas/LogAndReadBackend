using System;
using LogAndReadBackEnd.Entities;
using LogAndReadBackEnd.Services;
using Xunit;

namespace UnitTest
{
    public class UnitTest1
    {
        private readonly IUserService _userService;

        public UnitTest1()
        {
            _userService = new UserService(new MockRepository<WebUser>());
        }

        [Fact]
        public void WebUserIsCreatedCorrectly()
        {
            var userName = "Kajus";
            var password = "Slaptas123";

            var user = _userService.CreateWebUser(userName, password);

            Assert.NotNull(user);

            user = _userService.CreateWebUser(userName, password);

            Assert.Null(user);
        }
    }
}
