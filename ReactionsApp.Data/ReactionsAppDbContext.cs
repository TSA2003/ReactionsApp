using Microsoft.EntityFrameworkCore;
using ReactionsApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReactionsApp.Data
{
    public class ReactionsAppDbContext : DbContext
    {
        public ReactionsAppDbContext(DbContextOptions<ReactionsAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<StartingLightsGameResult> StartingLightsGameResults { get; set; }
        public DbSet<RandomPointsGameResult> RandomPointsGameResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }
    }
}
