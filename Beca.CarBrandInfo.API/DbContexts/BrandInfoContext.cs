using Beca.CarBrandInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Beca.CarBrandInfo.API.DbContexts
{
    public class BrandInfoContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Model> Models { get; set; } = null!;

        public BrandInfoContext(DbContextOptions<BrandInfoContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasData(
                new Brand("Ford")
                {
                    Id = 1
                },
                new Brand("Peugeot")
                {
                    Id = 2
                }
            );

            modelBuilder.Entity<Model>().HasData(
                new Model("Fiesta")
                {
                    Id = 1,
                    BrandId = 1,
                    Description = "Since 1976 to 2022. Ford has stopped production of this model."
                },
                new Model("405")
                {
                    Id = 2,
                    BrandId = 2,
                    Description = "Since 1987 to 1997. This model was replace in 2004 by Peugeot 407."
                }
            );

            base.OnModelCreating(modelBuilder);

        }
    }
}
