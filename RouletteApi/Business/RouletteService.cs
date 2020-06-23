using RouletteApi.Entities;
using System.Threading.Tasks;

namespace RouletteApi.Business
{
    interface RouletteService
    {
        Task<Roulette> AddRoulette();
        Task OpenRoulette(long idRoulette);
    }
}
