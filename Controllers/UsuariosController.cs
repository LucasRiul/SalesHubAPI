using DesafioBackEnd.Data;
using DesafioBackEnd.Models;
using DesafioBackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEnd.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UsuariosController : ControllerBase
    {
        private FrutariaContext _context = new FrutariaContext();

        [HttpGet]
        [Route("usuarios")]
        public async Task<IActionResult> GetAsync()
        {
            var usuarios = await _context.USUARIOS.AsNoTracking().ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet]
        [Route("usuarios/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var usuario = await _context.USUARIOS.AsNoTracking().FirstOrDefaultAsync(x => x.idUsuario == id);
            return usuario == null ? NotFound() : Ok(usuario);
        }

        [HttpPost("usuarios")]
        public async Task<IActionResult> PostAsync(
            [FromBody] USUARIOS model)
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

        [HttpPut("usuarios/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromBody] USUARIOS model,   
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var usuario = await _context.USUARIOS.FirstOrDefaultAsync(x => x.idUsuario == id);

            if (usuario == null)
                return NotFound();

            try
            {
                usuario.CPF = model.CPF;
                usuario.DataNascimento = model.DataNascimento;
                usuario.idEmpresa = model.idEmpresa;
                usuario.Login = model.Login;
                usuario.Nivel = model.Nivel;
                usuario.Nome = model.Nome;
                usuario.Senha = CriptografiaService.HashPassword(model.Senha);
                usuario.Status = model.Status;
                
                _context.USUARIOS.Update(usuario);
                await _context.SaveChangesAsync();
                return Ok(usuario);
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message);
            }
        }

        [HttpDelete("usuarios/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id)
        {
            var usuario = await _context.USUARIOS.FirstOrDefaultAsync(x => x.idUsuario == id);

            if (usuario == null)
                return NotFound();

            try
            {
                usuario.Status = false;
                _context.USUARIOS.Update(usuario);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message);

            }
        }
    }
}
