﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouletteApi.Entities
{
    public partial class Roulette
    {
        [Key]
        [Column(Order = 1, TypeName = "serial")]
        public long Id { get; set; }
        public string State { get; set; }
    }
}
