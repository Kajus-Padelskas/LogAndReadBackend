using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LogAndReadBackEnd.Data;
using LogAndReadBackEnd.DTOs;
using LogAndReadBackEnd.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LogAndReadBackEnd.Controllers
{
    public class UserController : BaseController
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("/user")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<WebUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [Authorize]
        [HttpGet("/user/{id}")]
        public async Task<ActionResult<WebUser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        [HttpPut("/user/{id}/update")]
        public async Task<ActionResult<WebUser>> UpdateUser(int id, [FromBody]UserDto user)
        {
            WebUser userToUpdate = await _context.Users.SingleOrDefaultAsync(webUser => webUser.Id == id);
            if (userToUpdate.Username != user.Username)
            {
                if (await UserExists(user.Username))
                {
                    return Unauthorized("User already exists by the username");
                }
                userToUpdate.Username = user.Username;
            }
            if (userToUpdate.Password != null)
            {
                using var hmac = new HMACSHA512();
                userToUpdate.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                userToUpdate.PasswordSalt = hmac.Key;
            }
            await _context.SaveChangesAsync();

            return userToUpdate;
        }

        [HttpDelete("/user/{id}/delete")]
        public async Task<ActionResult<String>> DeleteUser(int id)
        {
            WebUser userToDelete = await _context.Users.SingleOrDefaultAsync(user => user.Id == id);
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
            return Ok("User Deleted");
        }

        private Task<bool> UserExists(string username)
        {
            return _context.Users.AnyAsync(user => user.Username == username);
        }
    }
}
