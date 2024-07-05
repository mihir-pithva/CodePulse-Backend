using Microsoft.AspNetCore.Identity;

namespace CodePulse.Repositories
{
    public interface ITokenRepository
    {
        public string CreateJwtToken(IdentityUser user,List<string> roles);
    }
}
