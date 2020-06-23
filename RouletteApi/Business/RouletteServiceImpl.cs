using Microsoft.IdentityModel.SecurityTokenService;
using RouletteApi.Context;
using RouletteApi.Entities;
using System;
using System.Threading.Tasks;

namespace RouletteApi.Business
{
    public class RouletteServiceImpl : RouletteService
    {
        private readonly onlinebettingContext _contextDataBase;

        public RouletteServiceImpl(onlinebettingContext contextDataBase)
        {
            _contextDataBase = contextDataBase;
        }

        public async Task<Roulette> AddRoulette()
        {
            try
            {
                Roulette roulette = new Roulette();
                await _contextDataBase.Roulette.AddAsync(roulette);
                await _contextDataBase.SaveChangesAsync();
                return roulette;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task OpenRoulette(long idRoulette)
        {
            try
            {
                Roulette roulette = await _contextDataBase.Roulette.FindAsync(idRoulette);
                if (roulette == null)
                    throw new Exception("La ruleta no se encuentra creada en el sistema");
                if (roulette.State.Equals(StateRoulette.States.Opened.ToString()))
                    throw new BadRequestException("Denegado, La ruleta ya se encuentra abierta");
                roulette.State = StateRoulette.States.Opened.ToString();
                _contextDataBase.Entry(roulette).Property("State").IsModified = true;
                await _contextDataBase.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
