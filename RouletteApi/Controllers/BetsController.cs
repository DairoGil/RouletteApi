using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.SecurityTokenService;
using RouletteApi.Business;
using RouletteApi.Context;
using RouletteApi.Entities;

namespace RouletteApi.Controllers
{
    [Route("bet")]
    [ApiController]
    public class BetsController : ControllerBase
    {
        private readonly BetService _betService;

        public BetsController(onlinebettingContext context)
        {
            _betService = new BetServiceImpl(context);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Bet>> CreateBet(Bet bet)
        {
            try
            {
                bet.IdUser = Request.Headers["idUser"];
                await _betService.CreateBet(bet);
                return Ok("Exitoso, se creo la apuesta");
            }
            catch (BadRequestException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
