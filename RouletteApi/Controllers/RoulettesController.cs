using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RouletteApi.Business;
using RouletteApi.Context;
using RouletteApi.Entities;

namespace RouletteApi.Controllers
{
    [Route("roulette")]
    [ApiController]
    public class RoulettesController : ControllerBase
    {
        private readonly RouletteService _rouletteService;

        public RoulettesController(onlinebettingContext context)
        {
            _rouletteService = new RouletteServiceImpl(context);
        }

        [HttpGet("create")]
        public async Task<IActionResult> CreateRoulette( )
        {
            Roulette rouletteR = await _rouletteService.AddRoulette();
            return Ok(rouletteR.Id);
        }

        [HttpGet("open/{idRoulette}")]
        public async Task<IActionResult> OpenRoulette(long idRoulette)
        {
            await _rouletteService.OpenRoulette(idRoulette);
            return Ok("Exitoso, se abrio la ruleta");
        }
    }
}
