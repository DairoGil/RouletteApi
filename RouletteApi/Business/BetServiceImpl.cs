using Microsoft.IdentityModel.SecurityTokenService;
using RouletteApi.Context;
using RouletteApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.Business
{
    public class BetServiceImpl: BetService
    {
        private readonly onlinebettingContext _contextDataBase;

        public BetServiceImpl(onlinebettingContext contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }

        public async Task CreateBet(Bet bet)
        {
            try
            {
                ValidateStructureBet(bet);
                await ValidateExistRoulette(bet);
                await _contextDataBase.Bet.AddAsync(bet);
                await _contextDataBase.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ValidateStructureBet(Bet bet)
        {
            string color = bet.Color.ToLower();
            if (bet.Number < 0 | bet.Number > 36)
                throw new BadRequestException("El numero a apostar debe estar entre 0 y 36");
            if (!color.Equals(ColorBet.color.negro) && !color.Equals(ColorBet.color.rojo))
                throw new BadRequestException("El color a apostar debe ser negro o rojo");
            if (bet.Amount <= 0 | bet.Amount > 10000)
                throw new BadRequestException("La apuesta debe ser mas de 0 dolares y maximo 10.000 dolares");
            if (String.IsNullOrEmpty(bet.IdUser))
                throw new BadRequestException("Se debe indicar el id del usuario que desea apostar");
            if (bet.Number != null && String.IsNullOrEmpty(color))
                throw new BadRequestException("No puede apostar a un numero y a un color a la vez");
            if (bet.IdRoulette == 0)
                throw new BadRequestException("Se debe enviar un id de ruleta valido");
        }

        public async Task ValidateExistRoulette(Bet bet)
        {
            Roulette roulette = await _contextDataBase.Roulette.FindAsync(bet.IdRoulette);
            if (roulette == null)
                throw new BadRequestException("La ruleta especificada no existe en el sistema");
            if (roulette.State.Equals(StateRoulette.States.Closed))
                throw new BadRequestException("La ruleta se encuentra cerrada");
        }
    }
}
