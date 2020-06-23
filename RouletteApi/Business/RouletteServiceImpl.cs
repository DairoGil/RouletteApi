using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.SecurityTokenService;
using RouletteApi.Context;
using RouletteApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Roulette>> ListRoulette()
        {
            try
            {
                return await _contextDataBase.Roulette.ToListAsync();
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

        public async Task<long> CloseRoulette(long idRoulette)
        {
            try
            {
                Roulette roulette = await _contextDataBase.Roulette.FindAsync(idRoulette);
                if (roulette == null)
                    throw new Exception("La ruleta no se encuentra creada en el sistema");
                if (roulette.State.Equals(StateRoulette.States.Closed.ToString()))
                    throw new BadRequestException("Denegado, La ruleta ya se encuentra cerrada");
                roulette.State = StateRoulette.States.Closed.ToString();
                _contextDataBase.Entry(roulette).Property("State").IsModified = true;
                long total = roulette.TotalAmountBet;
                roulette.TotalAmountBet = 0;
                _contextDataBase.Entry(roulette).Property("TotalAmountBet").IsModified = true;
                await _contextDataBase.SaveChangesAsync();
                return total;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
