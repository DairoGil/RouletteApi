using RouletteApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.Business
{
    interface BetService
    {
        Task CreateBet(Bet bet);
    }
}
