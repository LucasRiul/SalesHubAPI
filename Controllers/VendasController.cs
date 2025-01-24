using DesafioBackEnd.Data;
using DesafioBackEnd.Models;
using DesafioBackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace DesafioBackEnd.Controllers
{
    [ApiController]
    [Route("v1")]
    public class VendasController : ControllerBase
    {
        private FrutariaContext _context = new FrutariaContext();

        [HttpGet]
        [Route("vendas")]
        public async Task<IActionResult> GetAsync()
        {
            var vendas = await _context.VENDAS.AsNoTracking().ToListAsync();
            return Ok(vendas);
        }

        [HttpGet]
        [Route("vendas/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var venda = await _context.VENDAS.AsNoTracking().FirstOrDefaultAsync(x => x.idVenda == id);
            return venda == null ? NotFound() : Ok(venda);
        }

        [HttpGet("vendas/{idVenda}/produtos")]
        public async Task<IActionResult> GetProdutosDaVendaAsync(int idVenda)
        {
            var venda = _context.VENDAS
                .Include(v => v.PRODUTO_VENDAS)
                .ThenInclude(pv => pv.Produto)
                .FirstOrDefault(v => v.idVenda == idVenda);

            if (venda == null)
            {
                return NotFound();
            }

            var produtosNaVenda = venda.PRODUTO_VENDAS.Select(pv => pv.Produto).ToList();
            return Ok(produtosNaVenda);
        }

        [HttpPost("vendas")]
        public async Task<IActionResult> PostAsync(
            [FromBody] VENDAS model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var venda = new VENDAS
            {
                DataVenda = model.DataVenda,
                Pago = false,
                Valor = 0,
                idUsuario = model.idUsuario,
            };

            try
            {
                await _context.VENDAS.AddAsync(venda);
                await _context.SaveChangesAsync();
                return Created($"v1/vendas/{venda.idVenda}", venda);
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message);
            }
        }

        [HttpPut("vendas/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromBody] VENDAS model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var venda = await _context.VENDAS.FirstOrDefaultAsync(x => x.idVenda == id);

            if (venda == null)
                return NotFound();

            try
            {
                var vendaa = _context.VENDAS
                .Include(v => v.PRODUTO_VENDAS)
                .ThenInclude(pv => pv.Produto)
                .FirstOrDefault(v => v.idVenda == id);

                var produtosNaVenda = vendaa.PRODUTO_VENDAS.Select(pv => pv.Produto).ToList();

                model.DataVenda = model.DataVenda;
                model.Pago = model.Pago;
                model.Valor = produtosNaVenda.Any() ? produtosNaVenda.Sum(x => x.PrecoVenda) : 0 ;
                model.idUsuario = model.idUsuario;
                
                _context.VENDAS.Update(venda);
                await _context.SaveChangesAsync();
                return Ok(venda);
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message);
            }
        }

        [HttpPost("vendas/{idVenda}/adicionar-produto/{idProduto}")]
        public IActionResult AdicionarProdutoNaVenda(int idVenda, int idProduto, [FromBody] PRODUTO_VENDAS produtoVenda)
        {
            var venda =  _context.VENDAS.Include(v => v.PRODUTO_VENDAS).FirstOrDefault(v => v.idVenda == idVenda);
            var produto = _context.PRODUTOS.FirstOrDefault(p => p.idProduto == idProduto);

            if (venda == null || produto == null)
            {
                return NotFound();
            }

            produtoVenda.Venda = venda;
            produtoVenda.Produto = produto;

            venda.PRODUTO_VENDAS.Add(produtoVenda);
            _context.SaveChanges();

            return Ok(venda);
        }

        [HttpDelete("vendas/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id)
        {
            var venda = await _context.VENDAS.FirstOrDefaultAsync(x => x.idVenda == id);

            if (venda == null)
                return NotFound();

            try
            {
                _context.VENDAS.Remove(venda);
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
