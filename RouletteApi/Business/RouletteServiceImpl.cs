using RouletteApi.Context;
using RouletteApi.Entities;
using System;
using System.Threading.Tasks;

namespace RouletteApi.Business
{
    public class RouletteServiceImpl : RouletteService
    {
        private readonly onlinebettingContext _context;

        public RouletteServiceImpl(onlinebettingContext contex)
        {
            _context = contex;
        }

        public async Task<Roulette> AddRoulette(Roulette roulette)
        {
            try
            {
                _context.Roulette.Add(roulette);
                await _context.SaveChangesAsync();
                return roulette;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
