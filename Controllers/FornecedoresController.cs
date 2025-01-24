using DesafioBackEnd.Data;
using DesafioBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace DesafioBackEnd.Controllers
{
    [ApiController]
    [Route("v1")]
    public class FornecedoresController : ControllerBase
    {
        private FrutariaContext _context = new FrutariaContext();

        [HttpGet]
        [Route("fornecedores")]
        public async Task<IActionResult> GetAsync()
        {
            var fornecedores = await _context.FORNECEDORES.AsNoTracking().ToListAsync();
            return Ok(fornecedores);
        }

        [HttpGet]
        [Route("fornecedores/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var fornecedor = await _context.FORNECEDORES.AsNoTracking().FirstOrDefaultAsync(x => x.idFornecedor == id);
            return fornecedor == null ? NotFound() : Ok(fornecedor);
        }

        [HttpPost("fornecedores")]
        public async Task<IActionResult> PostAsync(
            [FromBody] FORNECEDORES model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var fornecedor = new FORNECEDORES
            {
                CEP = model.CEP,
                Logradouro = model.Logradouro,
                Endereco = model.Endereco,
                Bairro = model.Bairro,
                UF = model.UF,
                Pais = model.Pais,
                Nome = model.Nome,
            };

            try
            {
                await _context.FORNECEDORES.AddAsync(fornecedor);
                await _context.SaveChangesAsync();
                return Created($"v1/fornecedores/{fornecedor.idFornecedor}", fornecedor);
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message);
            }
        }

        [HttpPut("fornecedores/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromBody] FORNECEDORES model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var fornecedor = await _context.FORNECEDORES.FirstOrDefaultAsync(x => x.idFornecedor == id);

            if (fornecedor == null)
                return NotFound();

            try
            {
                fornecedor.CEP = model.CEP;
                fornecedor.Logradouro = model.Logradouro;
                fornecedor.Endereco = model.Endereco;
                fornecedor.Bairro = model.Bairro;
                fornecedor.UF = model.UF;
                fornecedor.Pais = model.Pais;
                fornecedor.Nome = model.Nome;
                fornecedor.Telefone1 = model.Telefone1;
                fornecedor.Telefone2 = model.Telefone2;
                fornecedor.Cidade = model.Cidade;
                fornecedor.CPNJ = model.CPNJ;

                _context.FORNECEDORES.Update(fornecedor);
                await _context.SaveChangesAsync();
                return Ok(fornecedor);
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message);
            }
        }

        [HttpDelete("fornecedores/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id)
        {
            var fornecedor = await _context.FORNECEDORES.FirstOrDefaultAsync(x => x.idFornecedor == id);

            try
            {
                _context.FORNECEDORES.Remove(fornecedor);
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
