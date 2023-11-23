using System.IdentityModel.Tokens.Jwt;

namespace Biblioteca.Api.Token
{
    public class TokenJWT
    {
        private JwtSecurityToken token;
        internal TokenJWT(JwtSecurityToken token)
        {
            this.token = token;
        }
        public DateTimeOffset ValidTo => token.ValidTo;
        public string value => new JwtSecurityTokenHandler().WriteToken(this.token);  
    }
}
