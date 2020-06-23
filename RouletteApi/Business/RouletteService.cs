using RouletteApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RouletteApi.Business
{
    interface RouletteService
    {
        Task<Roulette> AddRoulette();
        Task OpenRoulette(long idRoulette);
        Task<List<Roulette>> ListRoulette();
        Task<long> CloseRoulette(long idRoulette);
    }
}
