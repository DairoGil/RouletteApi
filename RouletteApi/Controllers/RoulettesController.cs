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
        private readonly RouletteService rouletteService;

        public RoulettesController(onlinebettingContext context)
        {
            rouletteService = new RouletteServiceImpl(_context);
        }

        [HttpPost]
        public async Task<IActionResult> PostRoulette(Roulette roulette)
        {
            Roulette rouletteR = await rouletteService.AddRoulette(roulette);

            return Ok(rouletteR.Id);
        }
    }
}
