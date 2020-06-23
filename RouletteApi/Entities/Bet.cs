using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouletteApi.Entities
{
    public partial class Bet
    {
        public Bet()
        {
            Number = null;
        }

        [Key]
        [Column(Order = 1, TypeName = "serial")]
        public long Id { get; set; }
        public short? Number { get; set; }
        public string Color { get; set; }
        public short Amount { get; set; }
        public long IdRoulette { get; set; }
        public string IdUser { get; set; }
    }
}
