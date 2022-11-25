using LogAndReadBackEnd.Services;

namespace LogAndReadBackEnd.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DTOs;
    using Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet("/user")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<WebUser>>> GetUsers()
        {
            return this.Ok(this._userService.GetAllUsers());
        }

        [AllowAnonymous]
        [HttpGet("/user/{id}")]
        public async Task<ActionResult<WebUser>> GetUser(int id)
        {
            var user = this._userService.GetWebUser(user => user.Id == id);
            return user != null ? user : this.BadRequest("Error");
        }

        [HttpPut("/user/{id}/update")]
        public async Task<ActionResult<WebUser>> UpdateUser([FromBody]UserDto user)
        {
            var updatedUser = this._userService.UpdateWebUser(user);
            return updatedUser;
        }

        [HttpDelete("/user/{id}/delete")]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            this._userService.DeleteWebUser(id);
            return this.Ok("User Deleted");
        }
    }
}
