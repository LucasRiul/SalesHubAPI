using DesafioBackEnd.Data;
using DesafioBackEnd.Models;
using DesafioBackEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DesafioBackEnd.Controllers
{
    [ApiController]
    [Route("v1")]
    public class AuthController : ControllerBase
    {
        private FrutariaContext _context = new FrutariaContext();
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [Route("registrar")]
        public async Task<ActionResult<USUARIOS>> Registrar([FromBody] USUARIOS model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var usuario = new USUARIOS
            {
                CPF = model.CPF,
                DataCadastro = DateTime.Now,
                DataNascimento = model.DataNascimento,
                idEmpresa = model.idEmpresa,
                Login = model.Login,
                Nivel = model.Nivel,
                Nome = model.Nome,
                Senha = CriptografiaService.HashPassword(model.Senha),
                Status = true,
            };

            try
            {
                await _context.USUARIOS.AddAsync(usuario);
                await _context.SaveChangesAsync();
                return Created($"v1/usuarios/{usuario.idUsuario}", usuario);
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message);
            }


        }

        [HttpPost]
        [Route("login")]
        public ActionResult<USUARIOS> Login(USUARIOSDTO request)
        {
            var usuario = _context.USUARIOS.FirstOrDefault(x => x.Login == request.Login);
            if (usuario == null)
            {
                return BadRequest("Login inválido.");
            }

            if (!CriptografiaService.VerifyPassword(request.Senha, usuario.Senha))
            {
                return BadRequest("Login inválido.");
            }

            string token = CreateToken(usuario);
            return Ok(token);
        }

        private string CreateToken(USUARIOS usuario)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, usuario.Login),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
