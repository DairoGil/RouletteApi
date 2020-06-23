using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.SecurityTokenService;
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
            try {
                await _rouletteService.OpenRoulette(idRoulette);
                return Ok("Exitoso, se abrio la ruleta");
            }catch(BadRequestException exception)
            {
                return BadRequest(exception.Message);
            }
            
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListRoulette()
        {
            List<Roulette> listRoulette = await _rouletteService.ListRoulette();
            return Ok(listRoulette);
        }

        [HttpGet("close/{idRoulette}")]
        public async Task<IActionResult> CloseRoulette(long idRoulette)
        {
            try
            {
                long total  = await _rouletteService.CloseRoulette(idRoulette);
                return Ok($"Se ha cerrado exitosamente, el total de las apuestas es de {total}");
            }
            catch (BadRequestException exception)
            {
                return BadRequest(exception.Message);
            }

        }
    }
}
