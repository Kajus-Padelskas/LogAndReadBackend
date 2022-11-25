namespace LogAndReadBackEnd.Controllers
{
    using System.Threading.Tasks;
    using DTOs;
    using Services;
    using Microsoft.AspNetCore.Mvc;

    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AccountController(IUserService userService, ITokenService tokenService)
        {
            this._userService = userService;
            this._tokenService = tokenService;
        }

        [HttpPost("/register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            var user = this._userService.CreateWebUser(registerDto.Username, registerDto.Password);

            return user == null
                ? this.BadRequest("An error occurred")
                : new UserDto
                {
                    Username = user.Username,
                    Token = this._tokenService.CreateToken(user),
                };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LogIn([FromBody] LoginDto loginDto)
        {
            var user = this._userService.Login(loginDto.Username, loginDto.Password);

            return user == null
                ? this.BadRequest("An error occurred")
                : new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Token = this._tokenService.CreateToken(user),
                };
        }
    }
}