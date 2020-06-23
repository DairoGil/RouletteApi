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
        public async Task<IActionResult> CreateRoulette(Roulette roulette)
        {
            Roulette rouletteR = await _rouletteService.AddRoulette(roulette);

            return Ok(rouletteR.Id);
        }
    }
}
