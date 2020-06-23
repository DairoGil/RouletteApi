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

        public async Task<Roulette> AddRoulette(Roulette roulette)
        {
            try
            {
                _contextDataBase.Roulette.Add(roulette);
                await _contextDataBase.SaveChangesAsync();
                return roulette;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
