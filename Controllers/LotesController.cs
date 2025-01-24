using DesafioBackEnd.Data;
using DesafioBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace DesafioBackEnd.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LotesController : ControllerBase
    {
        private FrutariaContext _context = new FrutariaContext();

        [HttpGet]
        [Route("lotes")]
        public async Task<IActionResult> GetAsync()
        {
            var lotes = await _context.LOTES.AsNoTracking().ToListAsync();
            return Ok(lotes);
        }

        [HttpGet]
        [Route("lotes/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var lote = await _context.LOTES.AsNoTracking().FirstOrDefaultAsync(x => x.idLote == id);
            return lote == null ? NotFound() : Ok(lote);
        }

        [HttpPost("lotes")]
        public async Task<IActionResult> PostAsync(
            [FromBody] LOTES model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var lote = new LOTES
            {
                DataCadastro = DateTime.Now,
                Quantidade = model.Quantidade,
                idProduto = model.idProduto,
                DataValidade = model.DataValidade,                
                //Produto = 
            };

            try
            {
                await _context.LOTES.AddAsync(lote);
                await _context.SaveChangesAsync();
                return Created($"v1/lotes/{lote.idLote}", lote);
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message);
            }
        }

        [HttpPut("lotes/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromBody] LOTES model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var lote = await _context.LOTES.FirstOrDefaultAsync(x => x.idLote == id);

            if (lote == null)
                return NotFound();

            try
            {
                lote.DataValidade = model.DataValidade;
                lote.Quantidade = model.Quantidade;
                lote.idProduto = model.idProduto;

                _context.LOTES.Update(lote);
                await _context.SaveChangesAsync();
                return Ok(lote);
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message);
            }
        }

        [HttpDelete("lotes/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id)
        {
            var lote = await _context.LOTES.FirstOrDefaultAsync(x => x.idLote == id);

            try
            {
                _context.LOTES.Remove(lote);
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
