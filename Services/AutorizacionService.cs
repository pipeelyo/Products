using Microsoft.IdentityModel.Tokens;
using productos.Context;
using productos.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace productos.Services
{
    public class AutorizacionService : IAutorizacionService
    {
        private readonly AppDBContext _context;
        private readonly IConfiguration _configuration;

        public AutorizacionService(AppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GenerateToken(string idUsuario)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));

            var credencialesToken = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature
                );
            var tokenDescripcion = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(50),
                SigningCredentials = credencialesToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescripcion);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;
        }   
        public async Task<AutorizacionResponse> ReturnToken(AutorizacionRequest autorizacion)
        {
            var usuario_encontrado = _context.Usuarios.FirstOrDefault(x => 
                x.CorreoElectronico == autorizacion.CorreoElectronico &&
                x.Contrasena == autorizacion.Contrasena
            );

            if (usuario_encontrado == null) return await Task.FromResult<AutorizacionResponse>(null);

            string tokenCreado = GenerateToken(usuario_encontrado.UsuarioId.ToString());

            return new AutorizacionResponse() { Token = tokenCreado, Resultado = true, Mensaje = "Ok" };
        }
    }
}
