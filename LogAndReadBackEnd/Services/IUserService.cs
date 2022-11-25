namespace LogAndReadBackEnd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using DTOs;
    using Entities;

    public interface IUserService
    {
        public WebUser CreateWebUser(string username, string password);

        public WebUser UpdateWebUser(UserDto webUser);

        public void DeleteWebUser(int id);

        public WebUser GetWebUser(Expression<Func<WebUser, bool>> filter);

        public WebUser Login(string username, string password);

        public ICollection<WebUser> GetAllUsers();
    }
}
