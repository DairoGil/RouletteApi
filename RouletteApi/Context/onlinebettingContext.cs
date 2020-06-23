using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RouletteApi.Entities;

namespace RouletteApi.Context
{
    public partial class onlinebettingContext : DbContext
    {
        public onlinebettingContext()
        {
        }

        public onlinebettingContext(DbContextOptions<onlinebettingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bet> Bet { get; set; }
        public virtual DbSet<Roulette> Roulette { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bet>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasColumnType("character varying");

                entity.Property(e => e.IdRoulette).HasColumnName("idRoulette");

                entity.Property(e => e.IdUser)
                    .IsRequired()
                    .HasColumnName("idUser")
                    .HasColumnType("character varying");

                entity.Property(e => e.Number).HasColumnName("number");
            });

            modelBuilder.Entity<Roulette>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasColumnType("character varying");

                entity.Property(e => e.TotalAmountBet).HasColumnName("totalAmountBet");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
