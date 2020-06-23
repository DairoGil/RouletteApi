using Microsoft.IdentityModel.SecurityTokenService;
using RouletteApi.Context;
using RouletteApi.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.Business
{
    public class BetServiceImpl: BetService
    {
        private readonly onlinebettingContext _contextDataBase;
        private readonly IConnectionMultiplexer _redisImpl;

        public BetServiceImpl(onlinebettingContext contextDataBase, IConnectionMultiplexer redisImpl)
        {
            _contextDataBase = contextDataBase;
            _redisImpl = redisImpl;
        }

        public async Task CreateBet(Bet bet)
        {
            try
            {
                ValidateStructureBet(bet);
                string stateRoulette = await _redisImpl.GetDatabase().StringGetAsync($"{bet.IdRoulette}");
                ValidateExistRoulette(stateRoulette);
                Roulette roulette = await _contextDataBase.Roulette.FindAsync(bet.IdRoulette);
                await _contextDataBase.Bet.AddAsync(bet);
                roulette.TotalAmountBet = roulette.TotalAmountBet + bet.Amount;
                _contextDataBase.Entry(roulette).Property("TotalAmountBet").IsModified = true;
                await _contextDataBase.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ValidateStructureBet(Bet bet)
        {
            string color = bet.Color != null ? bet.Color.ToLower() : null;
            if (String.IsNullOrEmpty(color) && bet.Number == null)
                throw  new BadRequestException("Debe apostar a un color o a un numero");
            if (bet.Number < 0 | bet.Number > 36)
                throw new BadRequestException("El numero a apostar debe estar entre 0 y 36");
            if (!(color.Equals(ColorBet.color.negro.ToString()) | color.Equals(ColorBet.color.rojo.ToString())))
                throw new BadRequestException("El color a apostar debe ser negro o rojo");
            if (bet.Amount <= 0 | bet.Amount > 10000)
                throw new BadRequestException("La apuesta debe ser mas de 0 dolares y maximo 10.000 dolares");
            if (String.IsNullOrEmpty(bet.IdUser))
                throw new BadRequestException("Se debe indicar el id del usuario que desea apostar");
            if (bet.Number != null && !String.IsNullOrEmpty(color))
                throw new BadRequestException("No puede apostar a un numero y a un color a la vez");
            if (bet.IdRoulette == 0)
                throw new BadRequestException("Se debe enviar un id de ruleta valido");
        }

        public void ValidateExistRoulette(String stateRoulette)
        {
            if (String.IsNullOrEmpty(stateRoulette))
                throw new BadRequestException("La ruleta especificada no existe en el sistema");
            if (stateRoulette.Equals(StateRoulette.States.Closed.ToString()))
                throw new BadRequestException("La ruleta se encuentra cerrada");
        }
    }
}
