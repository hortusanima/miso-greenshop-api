using System.IdentityModel.Tokens.Jwt;

namespace miso_greenshop_api.Domain.Interfaces.Jwt
{
    public interface IJwtService
    {
        string Generate(string id);
        JwtSecurityToken Verify(string jwt);
    }
}
