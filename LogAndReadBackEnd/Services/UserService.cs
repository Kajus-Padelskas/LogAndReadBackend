namespace LogAndReadBackEnd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Security.Cryptography;
    using System.Text;
    using DTOs;
    using Entities;
    using Persistence;

    public class UserService : IUserService
    {
        private readonly IRepository<WebUser> _userRepository;

        public UserService(IRepository<WebUser> useRepository)
        {
            this._userRepository = useRepository;
        }

        public WebUser CreateWebUser(string username, string password)
        {
            if (this.UserExists(username))
            {
                return null;
            }

            this.HashUserPassword(password, out var passwordHash, out var passwordHashSalt);
            var user = new WebUser
            {
                Username = username,
                Password = passwordHash,
                PasswordSalt = passwordHashSalt,
                CreationTime = DateTime.Now,
            };

            this._userRepository.Add(user);
            this._userRepository.Save();
            return user;
        }

        public WebUser UpdateWebUser(UserDto webUser)
        {
            WebUser userToUpdate = this._userRepository.Get(user => user.Id == webUser.Id);
            this.HashUserPassword(webUser.Password, out var passwordHash, out var passwordHashSalt);
            userToUpdate.Username = webUser.Username;
            userToUpdate.Password = passwordHash;
            userToUpdate.PasswordSalt = passwordHashSalt;
            userToUpdate.CreationTime = DateTime.Now;
            this._userRepository.Save();
            return userToUpdate;
        }

        public void DeleteWebUser(int id)
        {
            var userToDelete = this._userRepository.Get(user => user.Id == id);
            this._userRepository.Delete(userToDelete);
            this._userRepository.Save();
        }

        public WebUser GetWebUser(Expression<Func<WebUser, bool>> filter)
        {
            return this._userRepository.Get(filter);
        }

        public WebUser Login(string username, string password)
        {
            var user = this._userRepository.Get(user => user.Username == username);
            if (user == null)
            {
                return null;
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            if (!computedHash.SequenceEqual(user.Password))
            {
                return null;
            }

            return user;
        }

        public ICollection<WebUser> GetAllUsers()
        {
            return this._userRepository.GetAll();
        }

        private bool UserExists(string username)
        {
            return this._userRepository.Get(user => user.Username == username) != null;
        }

        private void HashUserPassword(string password, out byte[] passwordHash, out byte[] passwordHashSalt)
        {
            using var hmac = new HMACSHA512();
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            passwordHashSalt = hmac.Key;
        }
    }
}
