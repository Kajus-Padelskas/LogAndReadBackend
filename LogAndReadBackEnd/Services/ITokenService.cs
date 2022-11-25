namespace LogAndReadBackEnd.Services
{
    using Entities;

    public interface ITokenService
    {
        public string CreateToken(WebUser user);
    }
}