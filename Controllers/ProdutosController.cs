using DesafioBackEnd.Data;
using DesafioBackEnd.Models;
using DesafioBackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEnd.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ProdutosController : ControllerBase
    {
        private FrutariaContext _context = new FrutariaContext();

        [HttpGet]
        [Route("produtos")]
        public async Task<IActionResult> GetAsync()
        {
            var produtos = await _context.PRODUTOS.AsNoTracking().ToListAsync();
            return Ok(produtos);
        }

        [HttpGet]
        [Route("produtos/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var produto = await _context.PRODUTOS.AsNoTracking().FirstOrDefaultAsync(x => x.idProduto == id);
            return produto == null ? NotFound() : Ok(produto);
        }

        [HttpPost("produtos")]
        public async Task<IActionResult> PostAsync(
            [FromBody] PRODUTOS model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var produto = new PRODUTOS
            {
                COFINS = model.COFINS,
                idUsuario = model.idUsuario,
                EstoqueAtual = model.EstoqueAtual,
                EstoqueInicial = model.EstoqueInicial,
                EstoqueMinimo = model.EstoqueMinimo,
                ICMS = model.ICMS,
                idFornecedor = model.idFornecedor,
                ISS = model.ISS,
                Nome = model.Nome,
                PrecoCusto = model.PrecoCusto,
                PrecoVenda = model.PrecoVenda,
                LUCRO = model.PrecoVenda - (model.PrecoCusto + model.COFINS + model.ICMS + model.ISS)
            };

            try
            {
                await _context.PRODUTOS.AddAsync(produto);
                await _context.SaveChangesAsync();
                return Created($"v1/produtos/{produto.idProduto}", produto);
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message);
            }
        }

        [HttpPut("produtos/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromBody] PRODUTOS model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var produto = await _context.PRODUTOS.FirstOrDefaultAsync(x => x.idProduto == id);

            if (produto == null)
                return NotFound();

            try
            {
                produto.COFINS = model.COFINS;
                produto.idUsuario = model.idUsuario;
                produto.EstoqueAtual = model.EstoqueAtual;
                produto.EstoqueInicial = model.EstoqueInicial;
                produto.EstoqueMinimo = model.EstoqueMinimo;
                produto.ICMS = model.ICMS;
                produto.idFornecedor = model.idFornecedor;
                produto.ISS = model.ISS;
                produto.Nome = model.Nome;
                produto.PrecoCusto = model.PrecoCusto;
                produto.PrecoVenda = model.PrecoVenda;
                produto.LUCRO = model.PrecoVenda - (model.PrecoCusto + model.COFINS + model.ICMS + model.ISS);

                _context.PRODUTOS.Update(produto);
                await _context.SaveChangesAsync();
                return Ok(produto);
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message);
            }
        }

        [HttpDelete("produtos/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id)
        {
            var produto = await _context.PRODUTOS.FirstOrDefaultAsync(x => x.idProduto == id);

            try
            {
                _context.PRODUTOS.Remove(produto);
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
