using LogAndReadBackEnd.Entities;

namespace LogAndReadBackEnd.Services
{
    public interface ITokenService
    {
        public string CreateToken(WebUser user);
    }
}