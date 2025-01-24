using DesafioBackEnd.Data;
using DesafioBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace DesafioBackEnd.Controllers
{
    [ApiController]
    [Route("v1")]
    public class EmpresasController : ControllerBase
    {
        private FrutariaContext _context = new FrutariaContext();

        [HttpGet]
        [Route("empresas")]
        public async Task<IActionResult> GetAsync()
        {
            var empresas = await _context.EMPRESAS.AsNoTracking().ToListAsync();
            return Ok(empresas);
        }

        [HttpGet]
        [Route("empresas/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var empresa = await _context.EMPRESAS.AsNoTracking().FirstOrDefaultAsync(x => x.idEmpresa == id);
            return empresa == null ? NotFound() : Ok(empresa);
        }

        [HttpPost("empresas")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EMPRESAS model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var empresa = new EMPRESAS
            {
                CNPJ = model.CNPJ,
                CEP = model.CEP,
                Logradouro = model.Logradouro,
                Endereco = model.Endereco,
                Bairro = model.Bairro,
                Cidade = model.Cidade,
                UF = model.UF,
                Pais = model.Pais,
                Nome = model.Nome,
                NomeFantasia = model.NomeFantasia,
                RegimeTributario = model.RegimeTributario,
            };

            try
            {
                await _context.EMPRESAS.AddAsync(empresa);
                await _context.SaveChangesAsync();
                return Created($"v1/empresas/{empresa.idEmpresa}", empresa);
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message);
            }
        }

        [HttpPut("empresas/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromBody] EMPRESAS model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var empresa = await _context.EMPRESAS.FirstOrDefaultAsync(x => x.idEmpresa == id);

            if (empresa == null)
                return NotFound();

            try
            {
                empresa.CNPJ = model.CNPJ;
                empresa.CEP = model.CEP;
                empresa.Cidade = model.Cidade;
                empresa.Logradouro = model.Logradouro;
                empresa.Endereco = model.Endereco;
                empresa.Bairro = model.Bairro;
                empresa.UF = model.UF;
                empresa.Pais = model.Pais;
                empresa.Nome = model.Nome;
                empresa.NomeFantasia = model.NomeFantasia;
                empresa.RegimeTributario = model.RegimeTributario;

                _context.EMPRESAS.Update(empresa);
                await _context.SaveChangesAsync();
                return Ok(empresa);
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message);
            }
        }

        [HttpDelete("empresas/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id)
        {
            var empresa = await _context.EMPRESAS.FirstOrDefaultAsync(x => x.idEmpresa == id);

            try
            {
                _context.EMPRESAS.Remove(empresa);
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
