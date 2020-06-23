using RouletteApi.Entities;
using System.Threading.Tasks;

namespace RouletteApi.Business
{
    interface RouletteService
    {
        Task<Roulette> AddRoulette(Roulette roulette);
        Task OpenRoulette(long idRoulette);
    }
}
